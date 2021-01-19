using System;
using System.Globalization;
using DeVes.Bazaar.Data.Contracts.Models;
using DeVes.Bazaar.Interfaces;
using DeVes.Bazaar.Logic;
using DeVes.Bazaar.Test.Wrapper;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using NUnit.Framework;

namespace DeVes.Bazaar.Test.Logic
{
    public class AccountingLogicTests
    {
        private IConfiguration   _configuration;
        private IArticleLogic    _articleLogic;
        private IAccountingLogic _accountingLogic;


        [SetUp]
        public void SetUp()
        {
            _configuration = Substitute.For<IConfiguration>();
            _articleLogic = Substitute.For<IArticleLogic>();
            _accountingLogic = new AccountingLogic(_configuration, _articleLogic);
        }


        [Test]
        public void Ctor_ConfigurationIsNull_ExpectException()
        {
            // ReSharper disable once ObjectCreationAsStatement
            var exception =Assert.Throws<ArgumentNullException>(() => new AccountingLogic(null, _articleLogic));
            Assert.AreEqual("Value cannot be null. (Parameter 'configuration')", exception.Message);
        }

        [Test]
        public void Ctor_ArticleLogicIsNull_ExpectException()
        {
            // ReSharper disable once ObjectCreationAsStatement
            var exception = Assert.Throws<ArgumentNullException>(() => new AccountingLogic(_configuration, null));
            Assert.AreEqual("Value cannot be null. (Parameter 'articleLogic')", exception.Message);
        }


        [TestCase(1)]
        [TestCase(5)]
        [TestCase(15)]
        public void GetAccountingList_NotKnownSellerNumber_ExpectEmptyResult(long sellerNumber)
        {
            var accountingList = _accountingLogic.GetAccountingList(sellerNumber);

            Assert.AreEqual(sellerNumber, accountingList.SellerNumber);
            Assert.AreEqual(0, accountingList.TurnoverGross);
            Assert.AreEqual(0, accountingList.TurnoverNet);
        }

        [TestCase(1, 0.0d, 16d)]
        [TestCase(5, 0.0d, 62.99d)]
        [TestCase(15, 0.0d, 62.99d)]
        [TestCase(1, 7.5d, 16d)]
        [TestCase(5, 7.5d, 62.99d)]
        [TestCase(15, 7.5d, 62.99d)]
        [TestCase(1, 15.0d, 16d)]
        [TestCase(5, 15.0d, 62.99d)]
        [TestCase(15, 15.0d, 62.99d)]
        public void GetAccountingList_KnownSellerNumber_ExpectResult(long sellerNumber, double tax, double revenueGross)
        {
            _configuration.GetSection("Tax").Returns(_ =>
                new ConfigurationSectionWrapper("tax", "", tax.ToString(CultureInfo.InvariantCulture)));
            _articleLogic.GetItems(Arg.Any<long>()).Returns(info =>
            {
                return sellerNumber switch
                {
                    1 => new[]
                    {
                        new ArticleModel {SoldFor = 5.0d}, new ArticleModel {SoldFor = 10.0d},
                        new ArticleModel {SoldFor = 1.0d},
                    },
                    _ => new[]
                    {
                        new ArticleModel {SoldFor = 19.99d}, new ArticleModel {SoldFor = 18.0d},
                        new ArticleModel {SoldFor = 25.0d},
                    }
                };
            });

            var accountingList = _accountingLogic.GetAccountingList(sellerNumber);

            var taxMoveInExpected     = accountingList.TurnoverGross * tax / 100;
            var turnoverGrossExpected = Math.Round(accountingList.TurnoverGross, 2);
            var turnoverNetExpected   = Math.Round(accountingList.TurnoverGross - taxMoveInExpected, 2);

            var turnoverGrossRound = Math.Round(accountingList.TurnoverGross, 2);
            var turnoverNetRound   = Math.Round(accountingList.TurnoverNet, 2);

            Assert.AreEqual(sellerNumber, accountingList.SellerNumber);
            Assert.AreEqual(turnoverGrossExpected, turnoverGrossRound);
            Assert.AreEqual(turnoverNetExpected, turnoverNetRound);
        }


        [TestCase(1)]
        [TestCase(5)]
        [TestCase(15)]
        public void ReturnRemainingArticles_NotKnownSellerNumber_NoUpdate(long sellerNumber)
        {
            _accountingLogic.ReturnRemainingArticles(sellerNumber);

            _articleLogic.DidNotReceive().Update(Arg.Any<ArticleModel>());
        }

        [TestCase(1)]
        [TestCase(5)]
        [TestCase(15)]
        public void ReturnRemainingArticles_KnownSellerNoSoldArticles_NoUpdate(long sellerNumber)
        {
            _articleLogic.GetItems(Arg.Any<long>()).Returns(info =>
                new[]
                {
                    new ArticleModel(),
                    new ArticleModel(),
                    new ArticleModel()
                });

            _accountingLogic.ReturnRemainingArticles(sellerNumber);

            _articleLogic.DidNotReceive().Update(Arg.Any<ArticleModel>());
        }

        [TestCase(1, 3)]
        [TestCase(5, 4)]
        [TestCase(10, 1)]
        [TestCase(15, 2)]
        public void ReturnRemainingArticles_KnownSellerAnySoldArticles_Update(long sellerNumber, int expectedUpdateCalls)
        {
            _articleLogic.GetItems(Arg.Any<long>()).Returns(info =>
            {
                return sellerNumber switch
                {
                    1 => new[]
                    {
                        new ArticleModel {SoldFor = 1.0d},
                        new ArticleModel (),
                        new ArticleModel {SoldFor = 1.0d}
                    },
                    5 => new[]
                    {
                        new ArticleModel {SoldFor = 5.0d},
                        new ArticleModel (),
                        new ArticleModel (),
                        new ArticleModel ()
                    },
                    10 => new[]
                    {
                        new ArticleModel {SoldFor = 1.0d}
                    },
                    15 => new[]
                    {
                        new ArticleModel (),
                        new ArticleModel ()
                    },
                    _ => new ArticleModel[0]
                };
            });

            _accountingLogic.ReturnRemainingArticles(sellerNumber);

            _articleLogic.Received(expectedUpdateCalls).Update(Arg.Any<ArticleModel>());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeVes.Bazaar.Contracts.Models;
using DeVes.Bazaar.Contracts.Repositories;
using NSubstitute;
using NUnit.Framework;

namespace DeVes.Bazaar.Logic.Tests
{
    public class SellerBillingLogicTests
    {
        private ISellerRepository  _sellerRepository;
        private IArticleRepository _articleRepository;
        private SellerBillingLogic _sellerBillingLogic;

        [SetUp]
        public void Setup()
        {
            _sellerRepository   = Substitute.For<ISellerRepository>();
            _articleRepository  = Substitute.For<IArticleRepository>();
            _sellerBillingLogic = new SellerBillingLogic(_sellerRepository, _articleRepository);
        }

        #region Ctor

        [Test]
        public void Ctor_injectSellerRepoNull_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new SellerBillingLogic(null, _articleRepository));
        }

        [Test]
        public void Ctor_injectArticleRepoNull_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new SellerBillingLogic(_sellerRepository, null));
        }

        [Test]
        public void Ctor_injectInstance_Instance()
        {
            Assert.NotNull(_sellerBillingLogic);
        }

        #endregion Ctor

        #region BillingAsync

        [TestCase(1)]
        [TestCase(3)]
        [TestCase(5)]
        public async Task BillingAsync_SellerNotKnown_SellerNotKnown(long sellerNumber)
        {
            var result = await _sellerBillingLogic.BillingAsync(sellerNumber);

            Assert.NotNull(result);
            Assert.AreEqual(sellerNumber, result.SellerNumber);
            Assert.AreEqual(0, result.Turnover);
            Assert.AreEqual(0d, result.Tax);
            Assert.AreEqual("SellerNotKnown", result.ErrorCode);
            Assert.IsNull(result.NowBilledArticle);
        }

        [TestCase(1)]
        [TestCase(3)]
        [TestCase(5)]
        public async Task BillingAsync_SellerKnownButNoArticles01_SellerNoUnBilledArticle(long sellerNumber)
        {
            _sellerRepository.GetItem(sellerNumber).Returns(new SellerModel());

            var result = await _sellerBillingLogic.BillingAsync(sellerNumber);

            Assert.NotNull(result);
            Assert.AreEqual(sellerNumber, result.SellerNumber);
            Assert.AreEqual(0, result.Turnover);
            Assert.AreEqual(0d, result.Tax);
            Assert.AreEqual("SellerNoUnBilledArticle", result.ErrorCode);
            Assert.IsNull(result.NowBilledArticle);
        }

        [TestCase(1)]
        [TestCase(3)]
        [TestCase(5)]
        public async Task BillingAsync_SellerKnownButNoArticles02_SellerNoUnBilledArticle(long sellerNumber)
        {
            _sellerRepository.GetItem(sellerNumber).Returns(new SellerModel());
            _articleRepository.GetItems(null, sellerNumber).Returns(new[]
            {
                new ArticleModel
                {
                    ReturnedAt = DateTime.Now
                },
                new ArticleModel
                {
                    ReturnedAt = DateTime.Now
                }
            });

            var result = await _sellerBillingLogic.BillingAsync(sellerNumber);

            Assert.NotNull(result);
            Assert.AreEqual(sellerNumber, result.SellerNumber);
            Assert.AreEqual(0, result.Turnover);
            Assert.AreEqual(0d, result.Tax);
            Assert.AreEqual("SellerNoUnBilledArticle", result.ErrorCode);
            Assert.IsNull(result.NowBilledArticle);
        }

        [TestCase(1)]
        [TestCase(3)]
        [TestCase(5)]
        public async Task BillingAsync_SellerKnownValidArticles_Bill(long sellerNumber)
        {
            _sellerRepository.GetItem(sellerNumber).Returns(new SellerModel
            {
                TaxPercent = 5
            });
            _articleRepository.GetItems(null, sellerNumber).Returns(new[]
            {
                new ArticleModel
                {
                    SoldFor    = 4,
                    ReturnedAt = DateTime.Now
                },
                new ArticleModel
                {
                    SoldAt  = DateTime.Now,
                    SoldFor = 8
                },
                new ArticleModel
                {
                    SoldFor    = 4,
                    ReturnedAt = DateTime.Now
                },
                new ArticleModel
                {
                    SoldAt  = DateTime.Now,
                    SoldFor = 18
                },
                new ArticleModel
                {
                }
            });


            var updateCounter = 0;
            var totalPrice    = 0.0d;
            _articleRepository.When(x => x.UpdateAsync(Arg.Any<long>(), Arg.Any<ArticleModel>()))
                              .Do(x =>
                              {
                                  if (!(x[1] is ArticleModel articleModel)) return;
                                  if (articleModel.SoldFor.HasValue is false) return;
                                  if (articleModel.ReturnedAt.HasValue is false) return;
                                  if (articleModel.ReturnedTax.HasValue is false) return;

                                  updateCounter++;
                                  totalPrice += articleModel.SoldFor.Value;
                              });


            var result = await _sellerBillingLogic.BillingAsync(sellerNumber);

            Assert.NotNull(result);
            Assert.AreEqual(sellerNumber, result.SellerNumber);
            Assert.AreEqual(26, result.Turnover);
            Assert.AreEqual(1.3d, result.Tax);
            Assert.IsNull(result.ErrorCode);
            Assert.AreEqual(3, result.NowBilledArticle.Count());

            Assert.AreEqual(2, updateCounter);
            Assert.AreEqual(26, totalPrice);
        }

        #endregion BillingAsync
    }
}
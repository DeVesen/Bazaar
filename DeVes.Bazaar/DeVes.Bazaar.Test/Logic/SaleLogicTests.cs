using System;
using DeVes.Bazaar.Data.Contracts.Models;
using DeVes.Bazaar.Dto;
using DeVes.Bazaar.Interfaces;
using DeVes.Bazaar.Logic;
using NSubstitute;
using NUnit.Framework;

namespace DeVes.Bazaar.Test.Logic
{
    public class SaleLogicTests
    {
        #region - Ctor -

        [Test]
        public void Ctor_Null_Exception()
        {
            // ReSharper disable once ObjectCreationAsStatement
            Assert.Throws<ArgumentNullException>(() => new SaleLogic(null));
        }

        [Test]
        public void Ctor_Repository_ValidInstance()
        {
            var articleLogic = Substitute.For<IArticleLogic>();

            Assert.NotNull(new SaleLogic(articleLogic));
        }

        #endregion - Ctor -


        #region - Sale -

        [Test]
        public void Sale_InsertNullPositions_ExpectException()
        {
            var articleLogic = Substitute.For<IArticleLogic>();
            var logic = new SaleLogic(articleLogic);

            var exception = Assert.Throws<ArgumentNullException>(() => logic.Sale(null));
            Assert.AreEqual("Value cannot be null. (Parameter 'salePositions')", exception.Message);
        }

        [Test]
        public void Sale_InsertEmptyPositions_ExpectNoCall()
        {
            var articleLogic = Substitute.For<IArticleLogic>();
            var logic = new SaleLogic(articleLogic);

            logic.Sale(new SaleDto[0]);

            articleLogic.Received(0).GetItem(Arg.Any<long>());
            articleLogic.Received(0).Update(Arg.Any<ArticleModel>());
        }

        [Test]
        public void Sale_InsertOneBadNumberItem01_ExpectException()
        {
            var articleLogic = Substitute.For<IArticleLogic>();
            var logic = new SaleLogic(articleLogic);

            var salePositions = new[]
            {
                new SaleDto {ArticleNumber = 0, SaleFor = 10}
            };

            var exception = Assert.Throws<ArgumentException>(() => logic.Sale(salePositions));
            Assert.AreEqual("Position 1: 'ArticleNumber' is not defined!", exception.Message);

            articleLogic.Received(0).GetItem(Arg.Any<long>());
            articleLogic.Received(0).Update(Arg.Any<ArticleModel>());
        }

        [Test]
        public void Sale_InsertOneBadNumberItem02_ExpectException()
        {
            var articleLogic = Substitute.For<IArticleLogic>();
            var logic = new SaleLogic(articleLogic);

            var salePositions = new[]
            {
                new SaleDto {ArticleNumber = 1, SaleFor = 10},
                new SaleDto {ArticleNumber = 0, SaleFor = 10},
            };

            articleLogic.GetItem(1).Returns(_ => new ArticleModel {OnSaleSince = DateTime.Now});

            var exception = Assert.Throws<ArgumentException>(() => logic.Sale(salePositions));
            Assert.AreEqual("Position 2: 'ArticleNumber' is not defined!", exception.Message);

            articleLogic.Received(1).GetItem(Arg.Any<long>());
            articleLogic.Received(0).Update(Arg.Any<ArticleModel>());
        }

        [Test]
        public void Sale_InsertOneBadNumberItem03_ExpectException()
        {
            var articleLogic = Substitute.For<IArticleLogic>();
            var logic = new SaleLogic(articleLogic);

            var salePositions = new[]
            {
                new SaleDto {ArticleNumber = 1, SaleFor = 10},
                new SaleDto {ArticleNumber = 2, SaleFor = 10},
                new SaleDto {ArticleNumber = 0, SaleFor = 10},
            };

            articleLogic.GetItem(1).Returns(_ => new ArticleModel {OnSaleSince = DateTime.Now});
            articleLogic.GetItem(2).Returns(_ => new ArticleModel { OnSaleSince = DateTime.Now });

            var exception = Assert.Throws<ArgumentException>(() => logic.Sale(salePositions));
            Assert.AreEqual("Position 3: 'ArticleNumber' is not defined!", exception.Message);

            articleLogic.Received(2).GetItem(Arg.Any<long>());
            articleLogic.Received(0).Update(Arg.Any<ArticleModel>());
        }

        [Test]
        public void Sale_InsertOneBadSaleForItem01_ExpectException()
        {
            var articleLogic = Substitute.For<IArticleLogic>();
            var logic = new SaleLogic(articleLogic);

            var salePositions = new[]
            {
                new SaleDto {ArticleNumber = 1, SaleFor = 0}
            };

            var exception = Assert.Throws<ArgumentException>(() => logic.Sale(salePositions));
            Assert.AreEqual("Position 1: 'SaleFor' is not defined!", exception.Message);

            articleLogic.Received(0).GetItem(Arg.Any<long>());
            articleLogic.Received(0).Update(Arg.Any<ArticleModel>());
        }

        [Test]
        public void Sale_InsertOneBadSaleForItem02_ExpectException()
        {
            var articleLogic = Substitute.For<IArticleLogic>();
            var logic = new SaleLogic(articleLogic);

            var salePositions = new[]
            {
                new SaleDto {ArticleNumber = 1, SaleFor = 10},
                new SaleDto {ArticleNumber = 2, SaleFor = 0},
            };

            articleLogic.GetItem(1).Returns(_ => new ArticleModel { OnSaleSince = DateTime.Now });

            var exception = Assert.Throws<ArgumentException>(() => logic.Sale(salePositions));
            Assert.AreEqual("Position 2: 'SaleFor' is not defined!", exception.Message);

            articleLogic.Received(1).GetItem(Arg.Any<long>());
            articleLogic.Received(0).Update(Arg.Any<ArticleModel>());
        }

        [Test]
        public void Sale_InsertOneBadSaleForItem03_ExpectException()
        {
            var articleLogic = Substitute.For<IArticleLogic>();
            var logic = new SaleLogic(articleLogic);

            var salePositions = new[]
            {
                new SaleDto {ArticleNumber = 1, SaleFor = 10},
                new SaleDto {ArticleNumber = 2, SaleFor = 10},
                new SaleDto {ArticleNumber = 3, SaleFor = 0},
            };

            articleLogic.GetItem(1).Returns(_ => new ArticleModel { OnSaleSince = DateTime.Now });
            articleLogic.GetItem(2).Returns(_ => new ArticleModel { OnSaleSince = DateTime.Now });

            var exception = Assert.Throws<ArgumentException>(() => logic.Sale(salePositions));
            Assert.AreEqual("Position 3: 'SaleFor' is not defined!", exception.Message);

            articleLogic.Received(2).GetItem(Arg.Any<long>());
            articleLogic.Received(0).Update(Arg.Any<ArticleModel>());
        }

        [Test]
        public void Sale_InsertNotKnownArticle01_ExpectException()
        {
            var articleLogic = Substitute.For<IArticleLogic>();
            var logic = new SaleLogic(articleLogic);

            var salePositions = new[]
            {
                new SaleDto {ArticleNumber = 1, SaleFor = 10}
            };

            var exception = Assert.Throws<ArgumentException>(() => logic.Sale(salePositions));
            Assert.AreEqual("Position 1: Article '1' not known!", exception.Message);

            articleLogic.Received(1).GetItem(Arg.Any<long>());
            articleLogic.Received(0).Update(Arg.Any<ArticleModel>());
        }

        [Test]
        public void Sale_InsertNotKnownArticle02_ExpectException()
        {
            var articleLogic = Substitute.For<IArticleLogic>();
            var logic = new SaleLogic(articleLogic);

            var salePositions = new[]
            {
                new SaleDto {ArticleNumber = 1, SaleFor = 10},
                new SaleDto {ArticleNumber = 2, SaleFor = 10}
            };

            articleLogic.GetItem(1).Returns(_ => new ArticleModel { OnSaleSince = DateTime.Now });

            var exception = Assert.Throws<ArgumentException>(() => logic.Sale(salePositions));
            Assert.AreEqual("Position 2: Article '2' not known!", exception.Message);

            articleLogic.Received(2).GetItem(Arg.Any<long>());
            articleLogic.Received(0).Update(Arg.Any<ArticleModel>());
        }

        [Test]
        public void Sale_InsertNotFreeArticle01_ExpectException()
        {
            var articleLogic = Substitute.For<IArticleLogic>();
            var logic = new SaleLogic(articleLogic);

            var salePositions = new[]
            {
                new SaleDto {ArticleNumber = 1, SaleFor = 10}
            };

            articleLogic.GetItem(1).Returns(_ => new ArticleModel());

            var exception = Assert.Throws<ArgumentException>(() => logic.Sale(salePositions));
            Assert.AreEqual("Position 1: Article '1' is not released for sale!", exception.Message);

            articleLogic.Received(1).GetItem(Arg.Any<long>());
            articleLogic.Received(0).Update(Arg.Any<ArticleModel>());
        }

        [Test]
        public void Sale_InsertNotFreeArticle02_ExpectException()
        {
            var articleLogic = Substitute.For<IArticleLogic>();
            var logic = new SaleLogic(articleLogic);

            var salePositions = new[]
            {
                new SaleDto {ArticleNumber = 1, SaleFor = 10},
                new SaleDto {ArticleNumber = 2, SaleFor = 10}
            };

            articleLogic.GetItem(1).Returns(_ => new ArticleModel { OnSaleSince = DateTime.Now });
            articleLogic.GetItem(2).Returns(_ => new ArticleModel());

            var exception = Assert.Throws<ArgumentException>(() => logic.Sale(salePositions));
            Assert.AreEqual("Position 2: Article '2' is not released for sale!", exception.Message);

            articleLogic.Received(2).GetItem(Arg.Any<long>());
            articleLogic.Received(0).Update(Arg.Any<ArticleModel>());
        }

        [Test]
        public void Sale_InsertAlreadySoldArticle01_ExpectException()
        {
            var articleLogic = Substitute.For<IArticleLogic>();
            var logic = new SaleLogic(articleLogic);

            var salePositions = new[]
            {
                new SaleDto {ArticleNumber = 1, SaleFor = 10}
            };

            articleLogic.GetItem(1).Returns(_ => new ArticleModel {OnSaleSince = DateTime.Now, SoldAt = DateTime.Now, SoldFor = 10});

            var exception = Assert.Throws<ArgumentException>(() => logic.Sale(salePositions));
            Assert.AreEqual("Position 1: Article '1' is already sold!", exception.Message);

            articleLogic.Received(1).GetItem(Arg.Any<long>());
            articleLogic.Received(0).Update(Arg.Any<ArticleModel>());
        }

        [Test]
        public void Sale_InsertAlreadySoldArticle02_ExpectException()
        {
            var articleLogic = Substitute.For<IArticleLogic>();
            var logic = new SaleLogic(articleLogic);

            var salePositions = new[]
            {
                new SaleDto {ArticleNumber = 1, SaleFor = 10},
                new SaleDto {ArticleNumber = 2, SaleFor = 10}
            };

            articleLogic.GetItem(1).Returns(_ => new ArticleModel { OnSaleSince = DateTime.Now });
            articleLogic.GetItem(2).Returns(_ => new ArticleModel { OnSaleSince = DateTime.Now, SoldAt = DateTime.Now, SoldFor = 10 });

            var exception = Assert.Throws<ArgumentException>(() => logic.Sale(salePositions));
            Assert.AreEqual("Position 2: Article '2' is already sold!", exception.Message);

            articleLogic.Received(2).GetItem(Arg.Any<long>());
            articleLogic.Received(0).Update(Arg.Any<ArticleModel>());
        }

        [Test]
        public void Sale_InsertAlreadyReturnedToSeller01_ExpectException()
        {
            var articleLogic = Substitute.For<IArticleLogic>();
            var logic = new SaleLogic(articleLogic);

            var salePositions = new[]
            {
                new SaleDto {ArticleNumber = 1, SaleFor = 10}
            };

            articleLogic.GetItem(1).Returns(_ => new ArticleModel {OnSaleSince = DateTime.Now, ReturnedAt = DateTime.Now});

            var exception = Assert.Throws<ArgumentException>(() => logic.Sale(salePositions));
            Assert.AreEqual("Position 1: Article '1' is already returned to Seller!", exception.Message);

            articleLogic.Received(1).GetItem(Arg.Any<long>());
            articleLogic.Received(0).Update(Arg.Any<ArticleModel>());
        }

        [Test]
        public void Sale_InsertAlreadyReturnedToSeller02_ExpectException()
        {
            var articleLogic = Substitute.For<IArticleLogic>();
            var logic = new SaleLogic(articleLogic);

            var salePositions = new[]
            {
                new SaleDto {ArticleNumber = 1, SaleFor = 10},
                new SaleDto {ArticleNumber = 2, SaleFor = 10}
            };

            articleLogic.GetItem(1).Returns(_ => new ArticleModel { OnSaleSince = DateTime.Now });
            articleLogic.GetItem(2).Returns(_ => new ArticleModel { OnSaleSince = DateTime.Now, ReturnedAt = DateTime.Now });

            var exception = Assert.Throws<ArgumentException>(() => logic.Sale(salePositions));
            Assert.AreEqual("Position 2: Article '2' is already returned to Seller!", exception.Message);

            articleLogic.Received(2).GetItem(Arg.Any<long>());
            articleLogic.Received(0).Update(Arg.Any<ArticleModel>());
        }

        [Test]
        public void Sale_InsertValidPositions01_ExpectUpdates()
        {
            var articleLogic = Substitute.For<IArticleLogic>();
            var logic = new SaleLogic(articleLogic);

            var salePositions = new[]
            {
                new SaleDto {ArticleNumber = 1, SaleFor = 10}
            };

            articleLogic.GetItem(1).Returns(_ => new ArticleModel { OnSaleSince = DateTime.Now });

            logic.Sale(salePositions);

            articleLogic.Received(1).GetItem(Arg.Any<long>());
            articleLogic.Received(1).Update(Arg.Any<ArticleModel>());
        }

        [Test]
        public void Sale_InsertValidPositions02_ExpectUpdates()
        {
            var articleLogic = Substitute.For<IArticleLogic>();
            var logic = new SaleLogic(articleLogic);

            var salePositions = new[]
            {
                new SaleDto {ArticleNumber = 1, SaleFor = 10},
                new SaleDto {ArticleNumber = 2, SaleFor = 10}
            };

            articleLogic.GetItem(1).Returns(_ => new ArticleModel { OnSaleSince = DateTime.Now });
            articleLogic.GetItem(2).Returns(_ => new ArticleModel { OnSaleSince = DateTime.Now });

            logic.Sale(salePositions);

            articleLogic.Received(2).GetItem(Arg.Any<long>());
            articleLogic.Received(2).Update(Arg.Any<ArticleModel>());
        }

        #endregion - Sale -
    }
}
using System;
using System.Linq;
using DeVes.Bazaar.Data.Contracts.Models;
using DeVes.Bazaar.Data.Contracts.Repositories;
using DeVes.Bazaar.Logic;
using NSubstitute;
using NUnit.Framework;
using CollectionAssert = Microsoft.VisualStudio.TestTools.UnitTesting.CollectionAssert;

namespace DeVes.Bazaar.Test.Logic
{
    public class ArticleLogicTests
    {
        #region - Ctor -

        [Test]
        public void Ctor_NullArticleRepository_Exception()
        {
            var sellerRepository = Substitute.For<ISellerRepository>();

            // ReSharper disable once ObjectCreationAsStatement
            Assert.Throws<ArgumentNullException>(() => new ArticleLogic(null, sellerRepository));
        }

        [Test]
        public void Ctor_NullSellerRepository_Exception()
        {
            var articleRepository = Substitute.For<IArticleRepository>();

            // ReSharper disable once ObjectCreationAsStatement
            Assert.Throws<ArgumentNullException>(() => new ArticleLogic(articleRepository, null));
        }

        [Test]
        public void Ctor_Repository_ValidInstance()
        {
            var articleRepository = Substitute.For<IArticleRepository>();
            var sellerRepository = Substitute.For<ISellerRepository>();

            Assert.NotNull(new ArticleLogic(articleRepository, sellerRepository));
        }

        #endregion - Ctor -


        #region - GetItems -

        [Test]
        public void GetItems_EmptyRepo_EmptyArray()
        {
            var articleRepository = Substitute.For<IArticleRepository>();
            var sellerRepository = Substitute.For<ISellerRepository>();
            var sut = new ArticleLogic(articleRepository, sellerRepository);

            Assert.IsEmpty(sut.GetItems());
        }

        [Test]
        public void GetItems_RepoHasItems_FilledArray()
        {
            var articleRepository = Substitute.For<IArticleRepository>();
            var sellerRepository = Substitute.For<ISellerRepository>();
            var logic = new ArticleLogic(articleRepository, sellerRepository);
            var tmpEntries = new[]
            {
                new ArticleModel(),
                new ArticleModel(),
                new ArticleModel(),
            };

            articleRepository.GetItems().Returns(tmpEntries);

            CollectionAssert.AreEqual(tmpEntries, logic.GetItems().ToArray());
        }

        #endregion - GetItem -


        #region - GetItem -

        [TestCase(1)]
        [TestCase(5)]
        [TestCase(10)]
        public void GetItem_EmptyRepo_Null(int number)
        {
            var articleRepository = Substitute.For<IArticleRepository>();
            var sellerRepository = Substitute.For<ISellerRepository>();
            var logic = new ArticleLogic(articleRepository, sellerRepository);

            Assert.IsNull(logic.GetItem(number));
        }

        [TestCase(1)]
        [TestCase(5)]
        [TestCase(10)]
        public void GetItem_RepoHasItems_ValidItem(int number)
        {
            var articleRepository = Substitute.For<IArticleRepository>();
            var sellerRepository = Substitute.For<ISellerRepository>();
            var logic = new ArticleLogic(articleRepository, sellerRepository);
            var tmpEntry = new ArticleModel { Number = number };

            articleRepository.GetItem(number).Returns(_ => tmpEntry);

            Assert.AreEqual(tmpEntry, logic.GetItem(number));
        }

        #endregion - GetItem -


        #region - Create -

        [Test]
        public void Create_BadObject_Exception()
        {
            var articleRepository = Substitute.For<IArticleRepository>();
            var sellerRepository = Substitute.For<ISellerRepository>();
            var logic = new ArticleLogic(articleRepository, sellerRepository);

            Assert.Throws<ArgumentNullException>(() => logic.Create(null));

            articleRepository.DidNotReceive().Insert(Arg.Any<ArticleModel>());
        }

        [TestCase(-10)]
        [TestCase(-5)]
        [TestCase(-1)]
        [TestCase(0)]
        public void Create_BadNumber_ExpectAutoNumber(long number)
        {
            var articleRepository = Substitute.For<IArticleRepository>();
            var sellerRepository = Substitute.For<ISellerRepository>();
            var logic = new ArticleLogic(articleRepository, sellerRepository);

            var testEntry = new ArticleModel
            {
                Number = number,
                SellerNumber = 1,
                Title = "xxx",
                Category = "yyy",
                Manufacturer = "zzz",
                Price = 10
            };

            sellerRepository.GetItem(1).Returns(_ => new SellerModel());

            logic.Create(testEntry);

            articleRepository.Received(1).Insert(Arg.Any<ArticleModel>());
        }

        [TestCase(-10)]
        [TestCase(-5)]
        [TestCase(-1)]
        [TestCase(0)]
        public void Create_BadTitle_ExpectException(long sellerNumber)
        {
            var articleRepository = Substitute.For<IArticleRepository>();
            var sellerRepository = Substitute.For<ISellerRepository>();
            var logic = new ArticleLogic(articleRepository, sellerRepository);

            var testEntry = new ArticleModel
            {
                Number = 1,
                SellerNumber = sellerNumber,
                Title = "xxx",
                Category = "yyy",
                Manufacturer = "zzz",
                Price = 10
            };

            var exception = Assert.Throws<ArgumentException>(() => logic.Create(testEntry));
            Assert.AreEqual("'SellerNumber' is not defined!", exception.Message);

            articleRepository.DidNotReceive().Insert(Arg.Any<ArticleModel>());
        }

        [Test]
        public void Create_BadTitle_ExpectException()
        {
            var articleRepository = Substitute.For<IArticleRepository>();
            var sellerRepository = Substitute.For<ISellerRepository>();
            var logic = new ArticleLogic(articleRepository, sellerRepository);

            var testEntry = new ArticleModel
            {
                Number = 1,
                SellerNumber = 1,
                Title = "",
                Category = "yyy",
                Manufacturer = "zzz",
                Price = 10
            };

            var exception = Assert.Throws<ArgumentException>(() => logic.Create(testEntry));
            Assert.AreEqual("'Title' is not defined!", exception.Message);

            articleRepository.DidNotReceive().Insert(Arg.Any<ArticleModel>());
        }

        [Test]
        public void Create_BadCategory_ExpectException()
        {
            var articleRepository = Substitute.For<IArticleRepository>();
            var sellerRepository = Substitute.For<ISellerRepository>();
            var logic = new ArticleLogic(articleRepository, sellerRepository);

            var testEntry = new ArticleModel
            {
                Number = 1,
                SellerNumber = 1,
                Title = "xxx",
                Category = "",
                Manufacturer = "zzz",
                Price = 10
            };

            var exception = Assert.Throws<ArgumentException>(() => logic.Create(testEntry));
            Assert.AreEqual("'Category' is not defined!", exception.Message);

            articleRepository.DidNotReceive().Insert(Arg.Any<ArticleModel>());
        }

        [Test]
        public void Create_BadManufacturer_ExpectException()
        {
            var articleRepository = Substitute.For<IArticleRepository>();
            var sellerRepository = Substitute.For<ISellerRepository>();
            var logic = new ArticleLogic(articleRepository, sellerRepository);

            var testEntry = new ArticleModel
            {
                Number = 1,
                SellerNumber = 1,
                Title = "xxx",
                Category = "yyy",
                Manufacturer = "",
                Price = 10
            };

            var exception = Assert.Throws<ArgumentException>(() => logic.Create(testEntry));
            Assert.AreEqual("'Manufacturer' is not defined!", exception.Message);

            articleRepository.DidNotReceive().Insert(Arg.Any<ArticleModel>());
        }

        [Test]
        public void Create_BadPrice_ExpectException()
        {
            var articleRepository = Substitute.For<IArticleRepository>();
            var sellerRepository = Substitute.For<ISellerRepository>();
            var logic = new ArticleLogic(articleRepository, sellerRepository);

            var testEntry = new ArticleModel
            {
                Number = 1,
                SellerNumber = 1,
                Title = "xxx",
                Category = "yyy",
                Manufacturer = "zzz",
                Price = 0
            };

            var exception = Assert.Throws<ArgumentException>(() => logic.Create(testEntry));
            Assert.AreEqual("'Price' is not defined!", exception.Message);

            articleRepository.DidNotReceive().Insert(Arg.Any<ArticleModel>());
        }

        [Test]
        public void Create_BadSoldAtGoodSoldFor_ExpectException()
        {
            var articleRepository = Substitute.For<IArticleRepository>();
            var sellerRepository = Substitute.For<ISellerRepository>();
            var logic = new ArticleLogic(articleRepository, sellerRepository);

            var testEntry = new ArticleModel
            {
                Number = 1,
                SellerNumber = 1,
                Title = "xxx",
                Category = "yyy",
                Manufacturer = "zzz",
                Price = 10,
                OnSaleSince = DateTime.Now,
                SoldAt = null,
                SoldFor = 10
            };

            var exception = Assert.Throws<ArgumentException>(() => logic.Create(testEntry));
            Assert.AreEqual("'SoldAt' is not defined!", exception.Message);

            articleRepository.DidNotReceive().Insert(Arg.Any<ArticleModel>());
        }

        [TestCase(-10)]
        [TestCase(-5)]
        [TestCase(-1)]
        [TestCase(0)]
        public void Create_BadSoldForGoodSoldAt_ExpectException(double soldFor)
        {
            var articleRepository = Substitute.For<IArticleRepository>();
            var sellerRepository = Substitute.For<ISellerRepository>();
            var logic = new ArticleLogic(articleRepository, sellerRepository);

            var testEntry = new ArticleModel
            {
                Number = 1,
                SellerNumber = 1,
                Title = "xxx",
                Category = "yyy",
                Manufacturer = "zzz",
                Price = 10,
                OnSaleSince = DateTime.Now,
                SoldAt = DateTime.Now,
                SoldFor = soldFor
            };

            var exception = Assert.Throws<ArgumentException>(() => logic.Create(testEntry));
            Assert.AreEqual("'SoldFor' is not defined!", exception.Message);

            articleRepository.DidNotReceive().Insert(Arg.Any<ArticleModel>());
        }

        [Test]
        public void Create_BadOnSaleSinceGoodSoldAtGoodSoldFor_ExpectException()
        {
            var articleRepository = Substitute.For<IArticleRepository>();
            var sellerRepository = Substitute.For<ISellerRepository>();
            var logic = new ArticleLogic(articleRepository, sellerRepository);

            var testEntry = new ArticleModel
            {
                Number = 1,
                SellerNumber = 1,
                Title = "xxx",
                Category = "yyy",
                Manufacturer = "zzz",
                Price = 10,
                OnSaleSince = null,
                SoldAt = DateTime.Now,
                SoldFor = 10
            };

            var exception = Assert.Throws<ArgumentException>(() => logic.Create(testEntry));
            Assert.AreEqual("'OnSaleSince' is not defined!", exception.Message);

            articleRepository.DidNotReceive().Insert(Arg.Any<ArticleModel>());
        }

        [Test]
        public void Create_ValidObjectNoSeller_ExpectException()
        {
            var articleRepository = Substitute.For<IArticleRepository>();
            var sellerRepository = Substitute.For<ISellerRepository>();
            var logic = new ArticleLogic(articleRepository, sellerRepository);

            var testEntry = new ArticleModel
            {
                Number = 1,
                SellerNumber = 1,
                Title = "xxx",
                Category = "yyy",
                Manufacturer = "zzz",
                Price = 10
            };

            var exception = Assert.Throws<ArgumentException>(() => logic.Create(testEntry));
            Assert.AreEqual("SellerNumber '1' not known!", exception.Message);
        }

        [Test]
        public void Create_ValidObject_ExpectCalls()
        {
            var articleRepository = Substitute.For<IArticleRepository>();
            var sellerRepository = Substitute.For<ISellerRepository>();
            var logic = new ArticleLogic(articleRepository, sellerRepository);

            var testEntry = new ArticleModel
            {
                Number = 1,
                SellerNumber = 1,
                Title = "xxx",
                Category = "yyy",
                Manufacturer = "zzz",
                Price = 10
            };

            sellerRepository.GetItem(1).Returns(_ => new SellerModel());

            logic.Create(testEntry);

            articleRepository.Received(1).Insert(testEntry);
        }

        #endregion - Create -


        #region - Update -

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-5)]
        [TestCase(-10)]
        public void Update_BadNumber_Exception(long number)
        {
            var articleRepository = Substitute.For<IArticleRepository>();
            var sellerRepository = Substitute.For<ISellerRepository>();
            var logic = new ArticleLogic(articleRepository, sellerRepository);

            var exception = Assert.Throws<ArgumentException>(() => logic.Update(new ArticleModel {Number = number }));
            Assert.AreEqual("'Number' is not defined!", exception.Message);

            articleRepository.DidNotReceive().Update(Arg.Any<ArticleModel>());
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-5)]
        [TestCase(-10)]
        public void Update_BadSellerNumber_ExpectException(long sellerNumber)
        {
            var articleRepository = Substitute.For<IArticleRepository>();
            var sellerRepository = Substitute.For<ISellerRepository>();
            var logic = new ArticleLogic(articleRepository, sellerRepository);

            var itemUpdate = new ArticleModel
            {
                Number = 1,
                SellerNumber = sellerNumber,
                Title = "xxx",
                Category = "yyy",
                Manufacturer = "zzz",
                Price = 10
            };

            articleRepository.GetItem(itemUpdate.Number).Returns(_ => new ArticleModel());

            var exception = Assert.Throws<ArgumentException>(() => logic.Update(itemUpdate));
            Assert.AreEqual("'SellerNumber' is not defined!", exception.Message);

            articleRepository.DidNotReceive().Update(Arg.Any<ArticleModel>());
        }

        [Test]
        public void Update_BadTitle_ExpectException()
        {
            var articleRepository = Substitute.For<IArticleRepository>();
            var sellerRepository = Substitute.For<ISellerRepository>();
            var logic = new ArticleLogic(articleRepository, sellerRepository);

            var itemUpdate = new ArticleModel
            {
                Number = 1,
                SellerNumber = 1,
                Title = "",
                Category = "yyy",
                Manufacturer = "zzz",
                Price = 10
            };

            articleRepository.GetItem(itemUpdate.Number).Returns(_ => new ArticleModel());

            var exception = Assert.Throws<ArgumentException>(() => logic.Update(itemUpdate));
            Assert.AreEqual("'Title' is not defined!", exception.Message);

            articleRepository.DidNotReceive().Update(Arg.Any<ArticleModel>());
        }

        [Test]
        public void Update_BadCategory_ExpectException()
        {
            var articleRepository = Substitute.For<IArticleRepository>();
            var sellerRepository = Substitute.For<ISellerRepository>();
            var logic = new ArticleLogic(articleRepository, sellerRepository);

            var itemUpdate = new ArticleModel
            {
                Number = 1,
                SellerNumber = 1,
                Title = "xxx",
                Category = "",
                Manufacturer = "zzz",
                Price = 10
            };

            articleRepository.GetItem(itemUpdate.Number).Returns(_ => new ArticleModel());

            var exception = Assert.Throws<ArgumentException>(() => logic.Update(itemUpdate));
            Assert.AreEqual("'Category' is not defined!", exception.Message);

            articleRepository.DidNotReceive().Update(Arg.Any<ArticleModel>());
        }

        [Test]
        public void Update_BadManufacturer_ExpectException()
        {
            var articleRepository = Substitute.For<IArticleRepository>();
            var sellerRepository = Substitute.For<ISellerRepository>();
            var logic = new ArticleLogic(articleRepository, sellerRepository);

            var itemUpdate = new ArticleModel
            {
                Number = 1,
                SellerNumber = 1,
                Title = "xxx",
                Category = "yyy",
                Manufacturer = "",
                Price = 10
            };

            articleRepository.GetItem(itemUpdate.Number).Returns(_ => new ArticleModel());

            var exception = Assert.Throws<ArgumentException>(() => logic.Update(itemUpdate));
            Assert.AreEqual("'Manufacturer' is not defined!", exception.Message);

            articleRepository.DidNotReceive().Update(Arg.Any<ArticleModel>());
        }

        [Test]
        public void Update_BadPrice_ExpectException()
        {
            var articleRepository = Substitute.For<IArticleRepository>();
            var sellerRepository = Substitute.For<ISellerRepository>();
            var logic = new ArticleLogic(articleRepository, sellerRepository);
            
            var itemUpdate = new ArticleModel
            {
                Number = 1,
                SellerNumber = 1,
                Title = "xxx",
                Category = "yyy",
                Manufacturer = "zzz",
                Price = 0
            };

            articleRepository.GetItem(itemUpdate.Number).Returns(_ => new ArticleModel());

            var exception = Assert.Throws<ArgumentException>(() => logic.Update(itemUpdate));
            Assert.AreEqual("'Price' is not defined!", exception.Message);

            articleRepository.DidNotReceive().Update(Arg.Any<ArticleModel>());
        }

        [Test]
        public void Update_ValidObjectNoSeller_ExpectException()
        {
            var articleRepository = Substitute.For<IArticleRepository>();
            var sellerRepository = Substitute.For<ISellerRepository>();
            var logic = new ArticleLogic(articleRepository, sellerRepository);

            var testEntry = new ArticleModel
            {
                Number = 1,
                SellerNumber = 1,
                Title = "xxx",
                Category = "yyy",
                Manufacturer = "zzz",
                Price = 10
            };

            articleRepository.GetItem(1).Returns(_ => new ArticleModel());

            var exception = Assert.Throws<ArgumentException>(() => logic.Update(testEntry));
            Assert.AreEqual("SellerNumber '1' not known!", exception.Message);
        }

        [Test]
        public void Update_ValidObject_ExpectCalls()
        {
            var articleRepository = Substitute.For<IArticleRepository>();
            var sellerRepository = Substitute.For<ISellerRepository>();
            var logic = new ArticleLogic(articleRepository, sellerRepository);

            var testEntry = new ArticleModel
            {
                Number = 1,
                SellerNumber = 1,
                Title = "xxx",
                Category = "yyy",
                Manufacturer = "zzz",
                Price = 10
            };

            articleRepository.GetItem(testEntry.Number).Returns(_ => new ArticleModel());
            sellerRepository.GetItem(1).Returns(_ => new SellerModel());

            logic.Update(testEntry);

            articleRepository.Received(1).Update(testEntry);
        }

        #endregion - Update -


        #region - Delete -

        [TestCase(1)]
        [TestCase(5)]
        [TestCase(10)]
        public void Delete_NotKnownNumbers_ExpectException(int number)
        {
            var articleRepository = Substitute.For<IArticleRepository>();
            var sellerRepository = Substitute.For<ISellerRepository>();
            var logic = new ArticleLogic(articleRepository, sellerRepository);

            Assert.Throws<ArgumentException>(() => logic.Delete(number));
        }

        [TestCase(1)]
        [TestCase(5)]
        [TestCase(10)]
        public void Delete_KnownNumbers_ExpectCall(int number)
        {
            var articleRepository = Substitute.For<IArticleRepository>();
            var sellerRepository = Substitute.For<ISellerRepository>();
            var logic = new ArticleLogic(articleRepository, sellerRepository);

            articleRepository.GetItem(number).Returns(_ => new ArticleModel());

            logic.Delete(number);

            articleRepository.Received(1).Delete(number);
        }

        #endregion - Delete -
    }
}
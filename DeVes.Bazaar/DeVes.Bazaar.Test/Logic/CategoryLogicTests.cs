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
    public class CategoryLogicTests
    {
        #region - Ctor -

        [Test]
        public void Ctor_Null_Exception()
        {
            // ReSharper disable once ObjectCreationAsStatement
            Assert.Throws<ArgumentNullException>(() => new CategoryLogic(null));
        }

        [Test]
        public void Ctor_Repository_ValidInstance()
        {
            var repository = Substitute.For<ICategoryRepository>();

            Assert.NotNull(new CategoryLogic(repository));
        }

        #endregion - Ctor -


        #region - GetItems -

        [Test]
        public void GetItems_EmptyRepo_EmptyArray()
        {
            var repository = Substitute.For<ICategoryRepository>();
            var sut = new CategoryLogic(repository);

            Assert.IsEmpty(sut.GetItems());
        }

        [Test]
        public void GetItems_RepoHasItems_FilledArray()
        {
            var repository = Substitute.For<ICategoryRepository>();
            var logic = new CategoryLogic(repository);
            var tmpEntries = new[]
            {
                new CategoryModel(),
                new CategoryModel(),
                new CategoryModel(),
            };

            repository.GetItems().Returns(tmpEntries);

            CollectionAssert.AreEqual(tmpEntries, logic.GetItems().ToArray());
        }

        #endregion - GetItem -


        #region - GetItem -

        [TestCase(1)]
        [TestCase(5)]
        [TestCase(10)]
        public void GetItem_EmptyRepo_Null(int number)
        {
            var repository = Substitute.For<ICategoryRepository>();
            var logic = new CategoryLogic(repository);

            Assert.IsNull(logic.GetItem(number));
        }

        [TestCase(1)]
        [TestCase(5)]
        [TestCase(10)]
        public void GetItem_RepoHasItems_ValidItem(int number)
        {
            var repository = Substitute.For<ICategoryRepository>();
            var logic = new CategoryLogic(repository);
            var tmpEntry = new CategoryModel { Number = number };

            repository.GetItem(number).Returns(_ => tmpEntry);

            Assert.AreEqual(tmpEntry, logic.GetItem(number));
        }

        #endregion - GetItem -


        #region - Create -

        [Test]
        public void Create_BadObject_Exception()
        {
            var repository = Substitute.For<ICategoryRepository>();
            var logic = new CategoryLogic(repository);

            Assert.Throws<ArgumentNullException>(() => logic.Create(null));

            repository.DidNotReceive().Insert(Arg.Any<CategoryModel>());
        }

        [TestCase(-10)]
        [TestCase(-5)]
        [TestCase(-1)]
        [TestCase(0)]
        public void Create_BadNumber_ExpectAutoNumber(int number)
        {
            var repository = Substitute.For<ICategoryRepository>();
            var logic = new CategoryLogic(repository);

            var testEntry = new CategoryModel
            {
                Number = number,
                Title = "xxx",
            };

            logic.Create(testEntry);

            repository.Received(1).Insert(Arg.Any<CategoryModel>());
        }

        [Test]
        public void Create_BadTitle_ExpectException()
        {
            var repository = Substitute.For<ICategoryRepository>();
            var logic = new CategoryLogic(repository);

            var testEntry = new CategoryModel
            {
                Number = 1,
                Title = "",
            };

            Assert.Throws<ArgumentException>(() => logic.Create(testEntry));

            repository.DidNotReceive().Insert(Arg.Any<CategoryModel>());
        }

        [Test]
        public void Create_ValidObject_ExpectCalls()
        {
            var repository = Substitute.For<ICategoryRepository>();
            var logic = new CategoryLogic(repository);

            var testEntry = new CategoryModel
            {
                Number = 1,
                Title = "xxx"
            };

            logic.Create(testEntry);

            repository.Received(1).Insert(testEntry);
        }

        #endregion - Create -


        #region - Update -

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-5)]
        [TestCase(-10)]
        public void Update_BadNumber_Exception(int number)
        {
            var repository = Substitute.For<ICategoryRepository>();
            var logic = new CategoryLogic(repository);

            Assert.Throws<ArgumentException>(() => logic.Update(new CategoryModel {Number = number }));

            repository.DidNotReceive().Update(Arg.Any<CategoryModel>());
        }

        [Test]
        public void Update_BadNumber_ExpectException()
        {
            var repository = Substitute.For<ICategoryRepository>();
            var logic = new CategoryLogic(repository);

            var itemUpdate = new CategoryModel
            {
                Number = 0,
                Title = "xxx"
            };

            repository.GetItem(itemUpdate.Number).Returns(_ => new CategoryModel());

            Assert.Throws<ArgumentException>(() => logic.Update(itemUpdate));

            repository.DidNotReceive().Update(Arg.Any<CategoryModel>());
        }

        [Test]
        public void Update_BadTitle_ExpectException()
        {
            var repository = Substitute.For<ICategoryRepository>();
            var logic = new CategoryLogic(repository);

            var itemUpdate = new CategoryModel
            {
                Number = 1,
                Title = ""
            };

            repository.GetItem(itemUpdate.Number).Returns(_ => new CategoryModel());

            Assert.Throws<ArgumentException>(() => logic.Update(itemUpdate));

            repository.DidNotReceive().Update(Arg.Any<CategoryModel>());
        }

        [Test]
        public void Update_ValidObject_ExpectCalls()
        {
            var repository = Substitute.For<ICategoryRepository>();
            var logic = new CategoryLogic(repository);

            var testEntry = new CategoryModel
            {
                Number = 1,
                Title = "xxx"
            };

            repository.GetItem(testEntry.Number).Returns(_ => new CategoryModel());

            logic.Update(testEntry);

            repository.Received(1).Update(testEntry);
        }

        #endregion - Update -


        #region - Delete -

        [TestCase(1)]
        [TestCase(5)]
        [TestCase(10)]
        public void Delete_NotKnownNumbers_ExpectException(int number)
        {
            var repository = Substitute.For<ICategoryRepository>();
            var logic = new CategoryLogic(repository);

            Assert.Throws<ArgumentException>(() => logic.Delete(number));
        }

        [TestCase(1)]
        [TestCase(5)]
        [TestCase(10)]
        public void Delete_KnownNumbers_ExpectCall(int number)
        {
            var repository = Substitute.For<ICategoryRepository>();
            var logic = new CategoryLogic(repository);

            repository.GetItem(number).Returns(_ => new CategoryModel());

            logic.Delete(number);

            repository.Received(1).Delete(number);
        }

        #endregion - Delete -
    }
}
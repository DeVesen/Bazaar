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
    public class ManufacturerLogicTests
    {
        #region - Ctor -

        [Test]
        public void Ctor_Null_Exception()
        {
            // ReSharper disable once ObjectCreationAsStatement
            Assert.Throws<ArgumentNullException>(() => new ManufacturerLogic(null));
        }

        [Test]
        public void Ctor_Repository_ValidInstance()
        {
            var repository = Substitute.For<IManufacturerRepository>();

            Assert.NotNull(new ManufacturerLogic(repository));
        }

        #endregion - Ctor -


        #region - GetItems -

        [Test]
        public void GetItems_EmptyRepo_EmptyArray()
        {
            var repository = Substitute.For<IManufacturerRepository>();
            var sut = new ManufacturerLogic(repository);

            Assert.IsEmpty(sut.GetItems());
        }

        [Test]
        public void GetItems_RepoHasItems_FilledArray()
        {
            var repository = Substitute.For<IManufacturerRepository>();
            var logic = new ManufacturerLogic(repository);
            var tmpEntries = new[]
            {
                new ManufacturerModel(),
                new ManufacturerModel(),
                new ManufacturerModel(),
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
            var repository = Substitute.For<IManufacturerRepository>();
            var logic = new ManufacturerLogic(repository);

            Assert.IsNull(logic.GetItem(number));
        }

        [TestCase(1)]
        [TestCase(5)]
        [TestCase(10)]
        public void GetItem_RepoHasItems_ValidItem(int number)
        {
            var repository = Substitute.For<IManufacturerRepository>();
            var logic = new ManufacturerLogic(repository);
            var tmpEntry = new ManufacturerModel { Number = number };

            repository.GetItem(number).Returns(_ => tmpEntry);

            Assert.AreEqual(tmpEntry, logic.GetItem(number));
        }

        #endregion - GetItem -


        #region - Create -

        [Test]
        public void Create_BadObject_Exception()
        {
            var repository = Substitute.For<IManufacturerRepository>();
            var logic = new ManufacturerLogic(repository);

            Assert.Throws<ArgumentNullException>(() => logic.Create(null));

            repository.DidNotReceive().Insert(Arg.Any<ManufacturerModel>());
        }

        [TestCase(-10)]
        [TestCase(-5)]
        [TestCase(-1)]
        [TestCase(0)]
        public void Create_BadNumber_ExpectAutoNumber(int number)
        {
            var repository = Substitute.For<IManufacturerRepository>();
            var logic = new ManufacturerLogic(repository);

            var testEntry = new ManufacturerModel
            {
                Number = number,
                Title = "xxx",
            };

            logic.Create(testEntry);

            repository.Received(1).Insert(Arg.Any<ManufacturerModel>());
        }

        [Test]
        public void Create_BadTitle_ExpectException()
        {
            var repository = Substitute.For<IManufacturerRepository>();
            var logic = new ManufacturerLogic(repository);

            var testEntry = new ManufacturerModel
            {
                Number = 1,
                Title = "",
            };

            Assert.Throws<ArgumentException>(() => logic.Create(testEntry));

            repository.DidNotReceive().Insert(Arg.Any<ManufacturerModel>());
        }

        [Test]
        public void Create_ValidObject_ExpectCalls()
        {
            var repository = Substitute.For<IManufacturerRepository>();
            var logic = new ManufacturerLogic(repository);

            var testEntry = new ManufacturerModel
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
            var repository = Substitute.For<IManufacturerRepository>();
            var logic = new ManufacturerLogic(repository);

            Assert.Throws<ArgumentException>(() => logic.Update(new ManufacturerModel {Number = number }));

            repository.DidNotReceive().Update(Arg.Any<ManufacturerModel>());
        }

        [Test]
        public void Update_BadNumber_ExpectException()
        {
            var repository = Substitute.For<IManufacturerRepository>();
            var logic = new ManufacturerLogic(repository);

            var itemUpdate = new ManufacturerModel
            {
                Number = 0,
                Title = "xxx"
            };

            repository.GetItem(itemUpdate.Number).Returns(_ => new ManufacturerModel());

            Assert.Throws<ArgumentException>(() => logic.Update(itemUpdate));

            repository.DidNotReceive().Update(Arg.Any<ManufacturerModel>());
        }

        [Test]
        public void Update_BadTitle_ExpectException()
        {
            var repository = Substitute.For<IManufacturerRepository>();
            var logic = new ManufacturerLogic(repository);

            var itemUpdate = new ManufacturerModel
            {
                Number = 1,
                Title = ""
            };

            repository.GetItem(itemUpdate.Number).Returns(_ => new ManufacturerModel());

            Assert.Throws<ArgumentException>(() => logic.Update(itemUpdate));

            repository.DidNotReceive().Update(Arg.Any<ManufacturerModel>());
        }

        [Test]
        public void Update_ValidObject_ExpectCalls()
        {
            var repository = Substitute.For<IManufacturerRepository>();
            var logic = new ManufacturerLogic(repository);

            var testEntry = new ManufacturerModel
            {
                Number = 1,
                Title = "xxx"
            };

            repository.GetItem(testEntry.Number).Returns(_ => new ManufacturerModel());

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
            var repository = Substitute.For<IManufacturerRepository>();
            var logic = new ManufacturerLogic(repository);

            Assert.Throws<ArgumentException>(() => logic.Delete(number));
        }

        [TestCase(1)]
        [TestCase(5)]
        [TestCase(10)]
        public void Delete_KnownNumbers_ExpectCall(int number)
        {
            var repository = Substitute.For<IManufacturerRepository>();
            var logic = new ManufacturerLogic(repository);

            repository.GetItem(number).Returns(_ => new ManufacturerModel());

            logic.Delete(number);

            repository.Received(1).Delete(number);
        }

        #endregion - Delete -
    }
}
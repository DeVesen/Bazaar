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
    public class SellerLogicTests
    {
        #region - Ctor -

        [Test]
        public void Ctor_Null_Exception()
        {
            // ReSharper disable once ObjectCreationAsStatement
            Assert.Throws<ArgumentNullException>(() => new SellerLogic(null));
        }

        [Test]
        public void Ctor_Repository_ValidInstance()
        {
            var repository = Substitute.For<ISellerRepository>();

            Assert.NotNull(new SellerLogic(repository));
        }

        #endregion - Ctor -


        #region - GetItems -

        [Test]
        public void GetItems_EmptyRepo_EmptyArray()
        {
            var repository = Substitute.For<ISellerRepository>();
            var sut = new SellerLogic(repository);

            Assert.IsEmpty(sut.GetItems());
        }

        [Test]
        public void GetItems_RepoHasItems_FilledArray()
        {
            var repository = Substitute.For<ISellerRepository>();
            var logic = new SellerLogic(repository);
            var tmpEntries = new[]
            {
                new SellerModel(),
                new SellerModel(),
                new SellerModel(),
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
            var repository = Substitute.For<ISellerRepository>();
            var logic = new SellerLogic(repository);

            Assert.IsNull(logic.GetItem(number));
        }

        [TestCase(1)]
        [TestCase(5)]
        [TestCase(10)]
        public void GetItem_RepoHasItems_ValidItem(int number)
        {
            var repository = Substitute.For<ISellerRepository>();
            var logic = new SellerLogic(repository);
            var tmpEntry = new SellerModel { Number = number };

            repository.GetItem(number).Returns(_ => tmpEntry);

            Assert.AreEqual(tmpEntry, logic.GetItem(number));
        }

        #endregion - GetItem -


        #region - Create -

        [Test]
        public void Create_BadObject_Exception()
        {
            var repository = Substitute.For<ISellerRepository>();
            var logic = new SellerLogic(repository);

            Assert.Throws<ArgumentNullException>(() => logic.Create(null));

            repository.DidNotReceive().Insert(Arg.Any<SellerModel>());
        }

        [TestCase(-10)]
        [TestCase(-5)]
        [TestCase(-1)]
        [TestCase(0)]
        public void Create_BadNumber_ExpectAutoNumber(int number)
        {
            var repository = Substitute.For<ISellerRepository>();
            var logic = new SellerLogic(repository);

            var testEntry = new SellerModel
            {
                Number = number,
                FirstName = "xxx",
                LastName = "yyy",
                Phone = "zzz"
            };

            logic.Create(testEntry);

            repository.Received(1).Insert(Arg.Any<SellerModel>());
        }

        [Test]
        public void Create_BadObjectBadFirstName_ExpectException()
        {
            var repository = Substitute.For<ISellerRepository>();
            var logic = new SellerLogic(repository);

            var testEntry = new SellerModel
            {
                Number = 1,
                FirstName = "",
                LastName = "yyy",
                Phone = "zzz"
            };

            Assert.Throws<ArgumentException>(() => logic.Create(testEntry));

            repository.DidNotReceive().Insert(Arg.Any<SellerModel>());
        }

        [Test]
        public void Create_BadObjectBadLastName_ExpectException()
        {
            var repository = Substitute.For<ISellerRepository>();
            var logic = new SellerLogic(repository);

            var testEntry = new SellerModel
            {
                Number = 1,
                FirstName = "xxx",
                LastName = "",
                Phone = "zzz"
            };

            Assert.Throws<ArgumentException>(() => logic.Create(testEntry));

            repository.DidNotReceive().Insert(Arg.Any<SellerModel>());
        }

        [Test]
        public void Create_BadObjectBadPhone_ExpectException()
        {
            var repository = Substitute.For<ISellerRepository>();
            var logic = new SellerLogic(repository);

            var testEntry = new SellerModel
            {
                Number = 1,
                FirstName = "xxx",
                LastName = "yyy",
                Phone = ""
            };

            Assert.Throws<ArgumentException>(() => logic.Create(testEntry));

            repository.DidNotReceive().Insert(Arg.Any<SellerModel>());
        }

        [Test]
        public void Create_ValidObject_ExpectCalls()
        {
            var repository = Substitute.For<ISellerRepository>();
            var logic = new SellerLogic(repository);

            var testEntry = new SellerModel
            {
                Number = 1,
                FirstName = "xxx",
                LastName = "yyy",
                Phone = "zzz"
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
            var repository = Substitute.For<ISellerRepository>();
            var logic = new SellerLogic(repository);

            Assert.Throws<ArgumentException>(() => logic.Update(new SellerModel {Number = number }));

            repository.DidNotReceive().Update(Arg.Any<SellerModel>());
        }

        [Test]
        public void Update_BadObjectBadNumber_ExpectException()
        {
            var repository = Substitute.For<ISellerRepository>();
            var logic = new SellerLogic(repository);

            var itemUpdate = new SellerModel
            {
                Number = 0,
                FirstName = "xxx",
                LastName = "yyy",
                Phone = "zzz"
            };

            repository.GetItem(itemUpdate.Number).Returns(_ => new SellerModel());

            Assert.Throws<ArgumentException>(() => logic.Update(itemUpdate));

            repository.DidNotReceive().Update(Arg.Any<SellerModel>());
        }

        [Test]
        public void Update_BadObjectBadFirstName_ExpectException()
        {
            var repository = Substitute.For<ISellerRepository>();
            var logic = new SellerLogic(repository);

            var itemUpdate = new SellerModel
            {
                Number = 1,
                FirstName = "",
                LastName = "yyy",
                Phone = "zzz"
            };

            repository.GetItem(itemUpdate.Number).Returns(_ => new SellerModel());

            Assert.Throws<ArgumentException>(() => logic.Update(itemUpdate));

            repository.DidNotReceive().Update(Arg.Any<SellerModel>());
        }

        [Test]
        public void Update_BadObjectBadLastName_ExpectException()
        {
            var repository = Substitute.For<ISellerRepository>();
            var logic = new SellerLogic(repository);

            var itemUpdate = new SellerModel
            {
                Number = 1,
                FirstName = "xxx",
                LastName = "",
                Phone = "zzz"
            };

            repository.GetItem(itemUpdate.Number).Returns(_ => new SellerModel());

            Assert.Throws<ArgumentException>(() => logic.Update(itemUpdate));

            repository.DidNotReceive().Update(Arg.Any<SellerModel>());
        }

        [Test]
        public void Update_BadObjectBadPhone_ExpectException()
        {
            var repository = Substitute.For<ISellerRepository>();
            var logic = new SellerLogic(repository);

            var itemUpdate = new SellerModel
            {
                Number = 1,
                FirstName = "xxx",
                LastName = "yyy",
                Phone = ""
            };

            repository.GetItem(itemUpdate.Number).Returns(_ => new SellerModel());

            Assert.Throws<ArgumentException>(() => logic.Update(itemUpdate));

            repository.DidNotReceive().Update(Arg.Any<SellerModel>());
        }

        [Test]
        public void Update_ValidObject_ExpectCalls()
        {
            var repository = Substitute.For<ISellerRepository>();
            var logic = new SellerLogic(repository);

            var testEntry = new SellerModel
            {
                Number = 1,
                FirstName = "xxx",
                LastName = "yyy",
                Phone = "zzz"
            };

            repository.GetItem(testEntry.Number).Returns(_ => new SellerModel());

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
            var repository = Substitute.For<ISellerRepository>();
            var logic = new SellerLogic(repository);

            Assert.Throws<ArgumentException>(() => logic.Delete(number));
        }

        [TestCase(1)]
        [TestCase(5)]
        [TestCase(10)]
        public void Delete_KnownNumbers_ExpectCall(int number)
        {
            var repository = Substitute.For<ISellerRepository>();
            var logic = new SellerLogic(repository);

            repository.GetItem(number).Returns(_ => new SellerModel());

            logic.Delete(number);

            repository.Received(1).Delete(number);
        }

        #endregion - Delete -
    }
}

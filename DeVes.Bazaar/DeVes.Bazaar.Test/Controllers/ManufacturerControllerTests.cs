using System;
using DeVes.Bazaar.Controllers;
using DeVes.Bazaar.Data.Contracts.Models;
using DeVes.Bazaar.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace DeVes.Bazaar.Test.Controllers
{
    public class ManufacturerControllerTests
    {
        #region - Ctor -

        [Test]
        public void Ctor_Null_Exception()
        {
            // ReSharper disable once ObjectCreationAsStatement
            Assert.Throws<ArgumentNullException>(() => new ManufacturerController(null));
        }

        [Test]
        public void Ctor_Repository_ValidInstance()
        {
            var repository = Substitute.For<IManufacturerLogic>();

            Assert.NotNull(new ManufacturerController(repository));
        }

        #endregion - Ctor -


        #region - Get() -

        [Test]
        public void Get_NoSellerItems_EmptyArray()
        {
            var repository = Substitute.For<IManufacturerLogic>();
            var sut = new ManufacturerController(repository);

            Assert.IsEmpty(sut.Get());
        }

        [Test]
        public void Get_SellerItems_ItemsArray()
        {
            var repository = Substitute.For<IManufacturerLogic>();
            var sut = new ManufacturerController(repository);

            var items = new[]
            {
                new ManufacturerModel(),
                new ManufacturerModel(),
                new ManufacturerModel(),
            };

            repository.GetItems().Returns(_ => items);

            Assert.AreEqual(items, sut.Get());
        }

        #endregion - Get() -


        #region - GetItem -

        [Test]
        public void Get_NoSellerItems_NullItem()
        {
            var repository = Substitute.For<IManufacturerLogic>();
            var sut = new ManufacturerController(repository);

            Assert.IsNull(sut.Get(1));
        }

        [Test]
        public void Get_SellerItems_Item()
        {
            var repository = Substitute.For<IManufacturerLogic>();
            var sut = new ManufacturerController(repository);

            var item = new ManufacturerModel();

            repository.GetItem(Arg.Any<long>()).Returns(_ => item);

            Assert.AreEqual(item, sut.Get(1));
        }

        #endregion - GetItem -


        #region - Create -

        [Test]
        public void Create_NullItem_Exception()
        {
            var repository = Substitute.For<IManufacturerLogic>();
            var sut = new ManufacturerController(repository);

            Assert.Throws<ArgumentNullException>(() => sut.Post(null));
        }

        [Test]
        public void Create_ValidModel_ExpectCallCreate()
        {
            var repository = Substitute.For<IManufacturerLogic>();
            var sut = new ManufacturerController(repository);

            var item = new ManufacturerModel();

            sut.Post(item);

            repository.Received(1).Create(Arg.Any<ManufacturerModel>());
        }

        #endregion - Create -


        #region - Update -

        [Test]
        public void Put_NullItem_Exception()
        {
            var repository = Substitute.For<IManufacturerLogic>();
            var sut = new ManufacturerController(repository);

            Assert.Throws<ArgumentNullException>(() => sut.Put(1, null));
        }

        [Test]
        public void Put_ValidModel_ExpectCallUpdate()
        {
            var repository = Substitute.For<IManufacturerLogic>();
            var sut = new ManufacturerController(repository);

            var item = new ManufacturerModel();

            sut.Put(1, item);

            repository.Received(1).Update(Arg.Any<ManufacturerModel>());
        }

        #endregion - Update -


        #region - Delete -

        [TestCase(-15)]
        [TestCase(-10)]
        [TestCase(-5)]
        [TestCase(-1)]
        [TestCase(0)]
        public void Delete_BadNumber_Exception(int number)
        {
            var repository = Substitute.For<IManufacturerLogic>();
            var sut = new ManufacturerController(repository);

            Assert.Throws<ArgumentException>(() => sut.Delete(number));
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(5)]
        [TestCase(10)]
        [TestCase(15)]
        public void Delete_ValidNumber_ExpectCallDelete(int number)
        {
            var repository = Substitute.For<IManufacturerLogic>();
            var sut = new ManufacturerController(repository);

            sut.Delete(number);

            repository.Received(1).Delete(number);
        }

        #endregion - Delete -
    }
}
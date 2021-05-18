using System;
using System.Linq;
using System.Threading.Tasks;
using DeVes.Bazaar.Contracts.Models;
using DeVes.Bazaar.Contracts.Repositories;
using NSubstitute;
using NUnit.Framework;

namespace DeVes.Bazaar.Logic.Tests
{
    public class SellerLogicTests
    {
        private ISellerRepository _sellerRepository;
        private SellerLogic       _sellerLogic;


        [SetUp]
        public void Setup()
        {
            _sellerRepository = Substitute.For<ISellerRepository>();
            _sellerLogic      = new SellerLogic(_sellerRepository);
        }


        #region Ctor

        [Test]
        public void Ctor_injectNull_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new SellerLogic(null));
        }

        [Test]
        public void Ctor_injectInstance_Instance()
        {
            Assert.NotNull(new SellerLogic(_sellerRepository));
        }

        #endregion Ctor

        #region GetItem

        [Test]
        public void GetItem_NoElementsBehind_Null()
        {
            var item = _sellerLogic.GetItem(1);
            Assert.IsNull(item);
        }

        [Test]
        public void GetItem_RequestedElementIsBehind_Element()
        {
            var expected = new SellerModel
            {
                Number    = 1,
                FirstName = "Hans",
                LastName  = "Steam"
            };

            _sellerRepository.GetItem(1).Returns(expected);

            var item = _sellerLogic.GetItem(1);
            Assert.IsNotNull(item);
            Assert.AreEqual(expected.Number, item.Number);
            Assert.AreEqual(expected.FirstName, item.FirstName);
            Assert.AreEqual(expected.LastName, item.LastName);
        }

        [Test]
        public void GetItem_RequestedElementIsNotBehind_Null()
        {
            _sellerRepository.GetItem(1).Returns(new SellerModel
            {
                Number    = 1,
                FirstName = "Hans",
                LastName  = "Steam"
            });

            var item = _sellerLogic.GetItem(2);
            Assert.IsNull(item);
        }

        #endregion GetItem

        #region GetItems

        [Test]
        public void GetItems_NoElementsBehind_EmptyResult()
        {
            var item = _sellerLogic.GetItems();
            Assert.IsEmpty(item);
        }

        [Test]
        public void GetItems_ElementsBehind_ElementsList()
        {
            var expected = new[]
            {
                new SellerModel
                {
                    Number    = 1,
                    FirstName = "Hans",
                    LastName  = "Steam"
                },
                new SellerModel
                {
                    Number    = 2,
                    FirstName = "Karl",
                    LastName  = "Josef"
                }
            };

            _sellerRepository.GetItems(null, null, null, null, null, null).Returns(expected);

            var items = _sellerLogic.GetItems().ToArray();
            Assert.IsNotNull(items);
            Assert.IsNotEmpty(items);
            Assert.AreEqual(expected[0].Number, items[0].Number);
            Assert.AreEqual(expected[0].FirstName, items[0].FirstName);
            Assert.AreEqual(expected[0].LastName, items[0].LastName);
            Assert.AreEqual(expected[1].Number, items[1].Number);
            Assert.AreEqual(expected[1].FirstName, items[1].FirstName);
            Assert.AreEqual(expected[1].LastName, items[1].LastName);
        }

        #endregion GetItems

        #region CreateAsync

        [Test]
        public void CreateAsync_InjectNull_ArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _sellerLogic.CreateAsync(null));
        }

        [TestCase(null)]
        [TestCase("")]
        public void CreateAsync_EmptyFirstName_ArgumentException(string value)
        {
            var sellerModel = new SellerModel
            {
                FirstName = value,
                LastName  = "Steam",
                Phone     = "4711"
            };
            var exception =
                Assert.ThrowsAsync<ArgumentException>(async () => await _sellerLogic.CreateAsync(sellerModel));
            Assert.AreEqual("'FirstName' is not defined!", exception.Message);
        }

        [TestCase(null)]
        [TestCase("")]
        public void CreateAsync_EmptyLastName_ArgumentException(string value)
        {
            var sellerModel = new SellerModel
            {
                FirstName = "Hans",
                LastName  = value,
                Phone     = "4711"
            };
            var exception =
                Assert.ThrowsAsync<ArgumentException>(async () => await _sellerLogic.CreateAsync(sellerModel));
            Assert.AreEqual("'LastName' is not defined!", exception.Message);
        }

        [TestCase(null)]
        [TestCase("")]
        public void CreateAsync_EmptyPhone_ArgumentException(string value)
        {
            var sellerModel = new SellerModel
            {
                FirstName = "Hans",
                LastName  = "Hans",
                Phone     = value
            };
            var exception =
                Assert.ThrowsAsync<ArgumentException>(async () => await _sellerLogic.CreateAsync(sellerModel));
            Assert.AreEqual("'Phone' is not defined!", exception.Message);
        }


        [TestCase(1)]
        [TestCase(3)]
        public async Task CreateAsync_NoNumber_CreatedElementWidthAutoId(long nextFreeNumber)
        {
            _sellerRepository.InsertAsync(Arg.Any<SellerModel>()).Returns(info =>
            {
                if (info[0] is SellerModel sellerModel)
                {
                    return Task.FromResult(sellerModel.Number == nextFreeNumber);
                }

                return Task.FromResult(false);
            });
            _sellerRepository.GetNextFreeNumber().Returns(nextFreeNumber);

            var item = new SellerModel
            {
                FirstName = "Hans",
                LastName  = "Steam",
                Phone     = "4711"
            };
            var result = await _sellerLogic.CreateAsync(item);

            Assert.IsTrue(result);
        }

        [TestCase(1)]
        [TestCase(3)]
        public async Task CreateAsync_Number_CreatedElementWidthAutoId(long expectedNumber)
        {
            _sellerRepository.InsertAsync(Arg.Any<SellerModel>()).Returns(info =>
            {
                if (info[0] is SellerModel sellerModel)
                {
                    return Task.FromResult(sellerModel.Number == expectedNumber);
                }

                return Task.FromResult(false);
            });
            _sellerRepository.GetNextFreeNumber().Returns(99);

            var item = new SellerModel
            {
                Number    = expectedNumber,
                FirstName = "Hans",
                LastName  = "Steam",
                Phone     = "4711"
            };
            var result = await _sellerLogic.CreateAsync(item);

            Assert.IsTrue(result);
        }

        #endregion CreateAsync

        #region UpdateAsync

        [Test]
        public void UpdateAsync_Null_ArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _sellerLogic.UpdateAsync(null));
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-3)]
        [TestCase(-5)]
        public void UpdateAsync_BadNumber_Exception(long number)
        {
            var item = new SellerModel
            {
                Number    = number,
                FirstName = "Hans",
                LastName  = "Steam"
            };

            var exception = Assert.ThrowsAsync<ArgumentException>(async () => await _sellerLogic.UpdateAsync(item));
            Assert.AreEqual("'Number' is not defined!", exception.Message);
        }

        [TestCase(null)]
        [TestCase("")]
        public void UpdateAsync_BadFirstName_Exception(string value)
        {
            var item = new SellerModel
            {
                Number    = 1,
                FirstName = value,
                LastName  = "Steam",
                Phone     = "4711"
            };

            var exception = Assert.ThrowsAsync<ArgumentException>(async () => await _sellerLogic.UpdateAsync(item));
            Assert.AreEqual("'FirstName' is not defined!", exception.Message);
        }

        [TestCase(null)]
        [TestCase("")]
        public void UpdateAsync_BadLastName_Exception(string value)
        {
            var item = new SellerModel
            {
                Number    = 1,
                FirstName = "Hans",
                LastName  = value,
                Phone     = "4711"
            };

            var exception = Assert.ThrowsAsync<ArgumentException>(async () => await _sellerLogic.UpdateAsync(item));
            Assert.AreEqual("'LastName' is not defined!", exception.Message);
        }

        [TestCase(null)]
        [TestCase("")]
        public void UpdateAsync_BadPhone_Exception(string value)
        {
            var item = new SellerModel
            {
                Number    = 1,
                FirstName = "Hans",
                LastName  = "Steam",
                Phone     = value
            };

            var exception = Assert.ThrowsAsync<ArgumentException>(async () => await _sellerLogic.UpdateAsync(item));
            Assert.AreEqual("'Phone' is not defined!", exception.Message);
        }

        [TestCase(1)]
        [TestCase(3)]
        [TestCase(5)]
        public void UpdateAsync_NotKnownNumber_Exception(long number)
        {
            var item = new SellerModel
            {
                Number    = number,
                FirstName = "Hans",
                LastName  = "Steam",
                Phone     = "4711"
            };

            var exception = Assert.ThrowsAsync<ArgumentException>(async () => await _sellerLogic.UpdateAsync(item));
            Assert.AreEqual($"Number '{number}' not in use!", exception.Message);
        }

        [TestCase(1)]
        [TestCase(3)]
        [TestCase(5)]
        public async Task UpdateAsync_KnownNumber_Exception(long number)
        {
            var item = new SellerModel
            {
                Number    = number,
                FirstName = "Hans",
                LastName  = "Steam",
                Phone     = "4711"
            };

            _sellerRepository.GetItem(number).Returns(new SellerModel());

            _sellerRepository.UpdateAsync(number, Arg.Any<SellerModel>())
                             .Returns(info => (long) info[0] == number &&
                                              info[1] is SellerModel);

            Assert.IsTrue(await _sellerLogic.UpdateAsync(item));
        }

        #endregion UpdateAsync

        #region DeleteAsync

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-3)]
        [TestCase(-5)]
        public void DeleteAsync_InjectBadNumber_ArgumentNullException(long number)
        {
            var exception = Assert.ThrowsAsync<ArgumentException>(async () => await _sellerLogic.DeleteAsync(number));
            Assert.AreEqual("'number' is not defined!", exception.Message);
        }

        [TestCase(1)]
        [TestCase(3)]
        [TestCase(5)]
        public async Task DeleteAsync_NotKnownElement_ArgumentException(long number)
        {
            _sellerRepository.DeleteAsync(number).Returns(info => (long) info[0] == number);

            Assert.IsTrue(await _sellerLogic.DeleteAsync(number));
        }

        #endregion DeleteAsync
    }
}
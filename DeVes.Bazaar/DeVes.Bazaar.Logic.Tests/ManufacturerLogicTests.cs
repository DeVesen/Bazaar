using System;
using System.Linq;
using System.Threading.Tasks;
using DeVes.Bazaar.Contracts.Models;
using DeVes.Bazaar.Contracts.Repositories;
using NSubstitute;
using NUnit.Framework;

namespace DeVes.Bazaar.Logic.Tests
{
    public class ManufacturerLogicTests
    {
        private IManufacturerRepository _manufacturerRepository;
        private ManufacturerLogic       _manufacturerLogic;


        [SetUp]
        public void Setup()
        {
            _manufacturerRepository = Substitute.For<IManufacturerRepository>();
            _manufacturerLogic      = new ManufacturerLogic(_manufacturerRepository);
        }


        #region Ctor

        [Test]
        public void Ctor_injectNull_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new ManufacturerLogic(null));
        }

        [Test]
        public void Ctor_injectInstance_Instance()
        {
            Assert.NotNull(new ManufacturerLogic(_manufacturerRepository));
        }

        #endregion Ctor

        #region GetItem

        [Test]
        public void GetItem_NoElementsBehind_Null()
        {
            var item = _manufacturerLogic.GetItem(1);
            Assert.IsNull(item);
        }

        [Test]
        public void GetItem_RequestedElementIsBehind_Element()
        {
            var expected = new ManufacturerModel
            {
                Number = 1,
                Title  = "Test Element"
            };

            _manufacturerRepository.GetItem(1).Returns(expected);

            var item = _manufacturerLogic.GetItem(1);
            Assert.IsNotNull(item);
            Assert.AreEqual(expected.Number, item.Number);
            Assert.AreEqual(expected.Title, item.Title);
        }

        [Test]
        public void GetItem_RequestedElementIsNotBehind_Null()
        {
            _manufacturerRepository.GetItem(1).Returns(new ManufacturerModel
            {
                Number = 1,
                Title  = "Test Element"
            });

            var item = _manufacturerLogic.GetItem(2);
            Assert.IsNull(item);
        }

        #endregion GetItem

        #region GetItems

        [Test]
        public void GetItems_NoElementsBehind_EmptyResult()
        {
            var item = _manufacturerLogic.GetItems();
            Assert.IsEmpty(item);
        }

        [Test]
        public void GetItems_ElementsBehind_ElementsList()
        {
            var expected = new[]
            {
                new ManufacturerModel
                {
                    Number = 1,
                    Title  = "Test Element #01"
                },
                new ManufacturerModel
                {
                    Number = 2,
                    Title  = "Test Element #02"
                }
            };

            _manufacturerRepository.GetItems().Returns(expected);

            var items = _manufacturerLogic.GetItems().ToArray();
            Assert.IsNotNull(items);
            Assert.IsNotEmpty(items);
            Assert.AreEqual(expected[0].Number, items[0].Number);
            Assert.AreEqual(expected[0].Title, items[0].Title);
            Assert.AreEqual(expected[1].Number, items[1].Number);
            Assert.AreEqual(expected[1].Title, items[1].Title);
        }

        #endregion GetItems

        #region CreateAsync

        [Test]
        public void CreateAsync_InjectNull_ArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _manufacturerLogic.CreateAsync(null));
        }

        [Test]
        public void CreateAsync_EmptyElement_ArgumentException()
        {
            var exception =
                Assert.ThrowsAsync<ArgumentException>(async () => await _manufacturerLogic.CreateAsync(new ManufacturerModel()));
            Assert.AreEqual("'Title' is not defined!", exception.Message);
        }


        [TestCase(1)]
        [TestCase(3)]
        public async Task CreateAsync_NoNumber_CreatedElementWidthAutoId(long nextFreeNumber)
        {
            _manufacturerRepository.InsertAsync(Arg.Any<ManufacturerModel>()).Returns(info =>
            {
                if (info[0] is ManufacturerModel ManufacturerModel)
                {
                    return Task.FromResult(ManufacturerModel.Number == nextFreeNumber);
                }
                return Task.FromResult(false);
            });
            _manufacturerRepository.GetNextFreeNumber().Returns(nextFreeNumber);

            var item = new ManufacturerModel
            {
                Title = "Test Article"
            };
            var result = await _manufacturerLogic.CreateAsync(item);

            Assert.IsTrue(result);
        }

        [TestCase(1)]
        [TestCase(3)]
        public async Task CreateAsync_Number_CreatedElementWidthAutoId(long expectedNumber)
        {
            _manufacturerRepository.InsertAsync(Arg.Any<ManufacturerModel>()).Returns(info =>
            {
                if (info[0] is ManufacturerModel ManufacturerModel)
                {
                    return Task.FromResult(ManufacturerModel.Number == expectedNumber);
                }
                return Task.FromResult(false);
            });
            _manufacturerRepository.GetNextFreeNumber().Returns(99);

            var item = new ManufacturerModel
            {
                Number = expectedNumber,
                Title  = "Test Article"
            };
            var result = await _manufacturerLogic.CreateAsync(item);

            Assert.IsTrue(result);
        }

        #endregion CreateAsync

        #region UpdateAsync

        [Test]
        public void UpdateAsync_Null_ArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _manufacturerLogic.UpdateAsync(null));
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-3)]
        [TestCase(-5)]
        public void UpdateAsync_0Number_Exception(long nextNumber)
        {
            var item = new ManufacturerModel
            {
                Number = nextNumber,
                Title  = "Test Article"
            };

            var exception = Assert.ThrowsAsync<ArgumentException>(async () => await _manufacturerLogic.UpdateAsync(item));
            Assert.AreEqual("'Number' is not defined!", exception.Message);
        }

        [TestCase(null)]
        [TestCase("")]
        public void UpdateAsync_BadTitle_Exception(string title)
        {
            var item = new ManufacturerModel
            {
                Number = 1,
                Title  = title
            };

            var exception = Assert.ThrowsAsync<ArgumentException>(async () => await _manufacturerLogic.UpdateAsync(item));
            Assert.AreEqual("'Title' is not defined!", exception.Message);
        }

        [TestCase(1)]
        [TestCase(3)]
        [TestCase(5)]
        public void UpdateAsync_NotKnownNumber_Exception(long number)
        {
            var item = new ManufacturerModel
            {
                Number = number,
                Title  = "Test Article"
            };

            var exception = Assert.ThrowsAsync<ArgumentException>(async () => await _manufacturerLogic.UpdateAsync(item));
            Assert.AreEqual($"Number '{number}' not in use!", exception.Message);
        }

        [TestCase(1)]
        [TestCase(3)]
        [TestCase(5)]
        public async Task UpdateAsync_KnownNumber_Exception(long number)
        {
            var item = new ManufacturerModel
            {
                Number = number,
                Title  = "Test Article"
            };

            _manufacturerRepository.GetItem(number).Returns(new ManufacturerModel());

            _manufacturerRepository.UpdateAsync(number, Arg.Any<ManufacturerModel>())
                                   .Returns(info => (long)info[0] == number &&
                                                    info[1] is ManufacturerModel);

            Assert.IsTrue(await _manufacturerLogic.UpdateAsync(item));
        }

        #endregion UpdateAsync

        #region DeleteAsync

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-3)]
        [TestCase(-5)]
        public void DeleteAsync_InjectBadNumber_ArgumentNullException(long number)
        {
            var exception = Assert.ThrowsAsync<ArgumentException>(async () => await _manufacturerLogic.DeleteAsync(number));
            Assert.AreEqual("'number' is not defined!", exception.Message);
        }

        [TestCase(1)]
        [TestCase(3)]
        [TestCase(5)]
        public async Task DeleteAsync_NotKnownElement_ArgumentException(long number)
        {
            _manufacturerRepository.DeleteAsync(number).Returns(info => (long)info[0] == number);

            Assert.IsTrue(await _manufacturerLogic.DeleteAsync(number));
        }

        #endregion DeleteAsync

        #region BasicInitializationAsync

        [Test]
        public async Task BasicInitializationAsync__MultiCallOfRepoInsert()
        {
            var counter = 0;
            _manufacturerRepository.When(x => x.InsertAsync(Arg.Any<ManufacturerModel>()))
                                   .Do(x => counter++);

            await _manufacturerLogic.BasicInitializationAsync();

            Assert.AreEqual(106, counter);
        }

        #endregion DeleteAsync
    }
}
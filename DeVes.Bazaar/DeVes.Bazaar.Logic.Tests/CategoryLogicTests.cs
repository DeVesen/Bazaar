using System;
using System.Linq;
using System.Threading.Tasks;
using DeVes.Bazaar.Contracts.Models;
using DeVes.Bazaar.Contracts.Repositories;
using NSubstitute;
using NSubstitute.Core.Arguments;
using NUnit.Framework;

namespace DeVes.Bazaar.Logic.Tests
{
    public class CategoryLogicTests
    {
        private ICategoryRepository _categoryRepository;
        private CategoryLogic       _categoryLogic;


        [SetUp]
        public void Setup()
        {
            _categoryRepository = Substitute.For<ICategoryRepository>();
            _categoryLogic = new CategoryLogic(_categoryRepository);
        }


        #region Ctor

        [Test]
        public void Ctor_injectNull_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new CategoryLogic(null));
        }

        [Test]
        public void Ctor_injectInstance_Instance()
        {
            Assert.NotNull(new CategoryLogic(_categoryRepository));
        }

        #endregion Ctor

        #region GetItem

        [Test]
        public void GetItem_NoElementsBehind_Null()
        {
            var item = _categoryLogic.GetItem(1);
            Assert.IsNull(item);
        }

        [Test]
        public void GetItem_RequestedElementIsBehind_Element()
        {
            var expected = new CategoryModel
            {
                Number = 1,
                Title  = "Test Element"
            };

            _categoryRepository.GetItem(1).Returns(expected);

            var item = _categoryLogic.GetItem(1);
            Assert.IsNotNull(item);
            Assert.AreEqual(expected.Number, item.Number);
            Assert.AreEqual(expected.Title, item.Title);
        }

        [Test]
        public void GetItem_RequestedElementIsNotBehind_Null()
        {
            _categoryRepository.GetItem(1).Returns(new CategoryModel
            {
                Number = 1,
                Title  = "Test Element"
            });

            var item = _categoryLogic.GetItem(2);
            Assert.IsNull(item);
        }

        #endregion GetItem

        #region GetItems

        [Test]
        public void GetItems_NoElementsBehind_EmptyResult()
        {
            var item = _categoryLogic.GetItems();
            Assert.IsEmpty(item);
        }

        [Test]
        public void GetItems_ElementsBehind_ElementsList()
        {
            var expected = new[]
            {
                new CategoryModel
                {
                    Number = 1,
                    Title  = "Test Element #01"
                },
                new CategoryModel
                {
                    Number = 2,
                    Title  = "Test Element #02"
                }
            };

            _categoryRepository.GetItems().Returns(expected);

            var items = _categoryLogic.GetItems().ToArray();
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
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _categoryLogic.CreateAsync(null));
        }

        [Test]
        public void CreateAsync_EmptyElement_ArgumentException()
        {
            var exception =
                Assert.ThrowsAsync<ArgumentException>(async () => await _categoryLogic.CreateAsync(new CategoryModel()));
            Assert.AreEqual("'Title' is not defined!", exception.Message);
        }


        [TestCase(1)]
        [TestCase(3)]
        public async Task CreateAsync_NoNumber_CreatedElementWidthAutoId(long nextFreeNumber)
        {
            _categoryRepository.InsertAsync(Arg.Any<CategoryModel>()).Returns(info =>
            {
                if (info[0] is CategoryModel categoryModel)
                {
                    return Task.FromResult(categoryModel.Number == nextFreeNumber);
                }
                return Task.FromResult(false);
            });
            _categoryRepository.GetNextFreeNumber().Returns(nextFreeNumber);

            var item = new CategoryModel
            {
                Title = "Test Article"
            };
            var result = await _categoryLogic.CreateAsync(item);

            Assert.IsTrue(result);
        }

        [TestCase(1)]
        [TestCase(3)]
        public async Task CreateAsync_Number_CreatedElementWidthAutoId(long expectedNumber)
        {
            _categoryRepository.InsertAsync(Arg.Any<CategoryModel>()).Returns(info =>
            {
                if (info[0] is CategoryModel categoryModel)
                {
                    return Task.FromResult(categoryModel.Number == expectedNumber);
                }
                return Task.FromResult(false);
            });
            _categoryRepository.GetNextFreeNumber().Returns(99);

            var item = new CategoryModel
            {
                Number = expectedNumber,
                Title = "Test Article"
            };
            var result = await _categoryLogic.CreateAsync(item);

            Assert.IsTrue(result);
        }

        #endregion CreateAsync

        #region UpdateAsync

        [Test]
        public void UpdateAsync_Null_ArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _categoryLogic.UpdateAsync(null));
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-3)]
        [TestCase(-5)]
        public void UpdateAsync_0Number_Exception(long nextNumber)
        {
            var item = new CategoryModel
            {
                Number = nextNumber,
                Title  = "Test Article"
            };

            var exception = Assert.ThrowsAsync<ArgumentException>(async () => await _categoryLogic.UpdateAsync(item));
            Assert.AreEqual("'Number' is not defined!", exception.Message);
        }

        [TestCase(null)]
        [TestCase("")]
        public void UpdateAsync_BadTitle_Exception(string title)
        {
            var item = new CategoryModel
            {
                Number = 1,
                Title  = title
            };

            var exception = Assert.ThrowsAsync<ArgumentException>(async () => await _categoryLogic.UpdateAsync(item));
            Assert.AreEqual("'Title' is not defined!", exception.Message);
        }

        [TestCase(1)]
        [TestCase(3)]
        [TestCase(5)]
        public void UpdateAsync_NotKnownNumber_Exception(long number)
        {
            var item = new CategoryModel
            {
                Number = number,
                Title = "Test Article"
            };

            var exception = Assert.ThrowsAsync<ArgumentException>(async () => await _categoryLogic.UpdateAsync(item));
            Assert.AreEqual($"Number '{number}' not in use!", exception.Message);
        }

        [TestCase(1)]
        [TestCase(3)]
        [TestCase(5)]
        public async Task UpdateAsync_KnownNumber_Exception(long number)
        {
            var item = new CategoryModel
            {
                Number = number,
                Title  = "Test Article"
            };

            _categoryRepository.GetItem(number).Returns(new CategoryModel());

            _categoryRepository.UpdateAsync(number, Arg.Any<CategoryModel>())
                               .Returns(info => (long)info[0] == number &&
                                                info[1] is CategoryModel);

            Assert.IsTrue(await _categoryLogic.UpdateAsync(item));
        }

        #endregion UpdateAsync

        #region DeleteAsync

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-3)]
        [TestCase(-5)]
        public void DeleteAsync_InjectBadNumber_ArgumentNullException(long number)
        {
            var exception = Assert.ThrowsAsync<ArgumentException>(async () => await _categoryLogic.DeleteAsync(number));
            Assert.AreEqual("'number' is not defined!", exception.Message);
        }

        [TestCase(1)]
        [TestCase(3)]
        [TestCase(5)]
        public async Task DeleteAsync_NotKnownElement_ArgumentException(long number)
        {
            _categoryRepository.DeleteAsync(number).Returns(info => (long) info[0] == number);

            Assert.IsTrue(await _categoryLogic.DeleteAsync(number));
        }

        #endregion DeleteAsync

        #region BasicInitializationAsync

        [Test]
        public async Task BasicInitializationAsync__MultiCallOfRepoInsert()
        {
            var counter = 0;
            _categoryRepository.When(x => x.InsertAsync(Arg.Any<CategoryModel>()))
               .Do(x => counter++);

            await _categoryLogic.BasicInitializationAsync();

            Assert.AreEqual(13, counter);
        }

        #endregion DeleteAsync
    }
}
using System;
using System.Linq;
using System.Threading.Tasks;
using DeVes.Bazaar.Contracts.Dto;
using DeVes.Bazaar.Contracts.Models;
using DeVes.Bazaar.Contracts.Repositories;
using NSubstitute;
using NUnit.Framework;

namespace DeVes.Bazaar.Logic.Tests
{
    public class ArticleLogicTests
    {
        private ISellerRepository  _sellerRepository;
        private IArticleRepository _articleRepository;
        private ArticleLogic       _articleLogic;


        [SetUp]
        public void Setup()
        {
            _sellerRepository  = Substitute.For<ISellerRepository>();
            _articleRepository = Substitute.For<IArticleRepository>();
            _articleLogic      = new ArticleLogic(_articleRepository, _sellerRepository);
        }


        #region Ctor

        [Test]
        public void Ctor_injectArticleRepositoryNull_ArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new ArticleLogic(null, _sellerRepository));
            Assert.AreEqual("Value cannot be null. (Parameter 'articleRepository')", exception.Message);
        }

        [Test]
        public void Ctor_injectSellerRepositoryNull_ArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => new ArticleLogic(_articleRepository, null));
            Assert.AreEqual("Value cannot be null. (Parameter 'sellerRepository')", exception.Message);
        }

        [Test]
        public void Ctor_injectInstance_Instance()
        {
            Assert.NotNull(_articleLogic);
        }

        #endregion Ctor

        #region GetItem

        [Test]
        public void GetItem_NoElementsBehind_Null()
        {
            var item = _articleLogic.GetItem(1);
            Assert.IsNull(item);
        }

        [Test]
        public void GetItem_RequestedElementIsBehind_Element()
        {
            var expected = new ArticleModel
            {
                Number = 1,
                Title  = "Test Element"
            };

            _articleRepository.GetItem(1).Returns(expected);

            var item = _articleLogic.GetItem(1);
            Assert.IsNotNull(item);
            Assert.AreEqual(expected.Number, item.Number);
            Assert.AreEqual(expected.Title, item.Title);
        }

        [Test]
        public void GetItem_RequestedElementIsNotBehind_Null()
        {
            _articleRepository.GetItem(1).Returns(new ArticleModel
            {
                Number = 1,
                Title  = "Test Element"
            });

            var item = _articleLogic.GetItem(2);
            Assert.IsNull(item);
        }

        #endregion GetItem

        #region GetItems

        [Test]
        public void GetItems_NoElementsBehind_EmptyResult()
        {
            var item = _articleLogic.GetItems(null, null, null, null, null, null, null);
            Assert.IsEmpty(item);
        }

        [Test]
        public void GetItems_ElementsBehind_ElementsList()
        {
            var expected = new[]
            {
                new ArticleModel
                {
                    Number = 1,
                    Title  = "Test Element #01"
                },
                new ArticleModel
                {
                    Number = 2,
                    Title  = "Test Element #02"
                }
            };

            _articleRepository.GetItems().Returns(expected);

            var items = _articleLogic.GetItems(null, null, null, null, null, null, null).ToArray();
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
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _articleLogic.CreateAsync(null));
        }

        [Test]
        public void CreateAsync_EmptyElement_ArgumentException()
        {
            Assert.ThrowsAsync<ArgumentException>(async () => await _articleLogic.CreateAsync(new ArticleInsertDto()));
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-5)]
        [TestCase(-9)]
        public void CreateAsync_BadSellerNumber_ArgumentException(long sellerNumber)
        {
            var exception = Assert.ThrowsAsync<ArgumentException>(async () =>
                                                                      await _articleLogic
                                                                          .CreateAsync(new ArticleInsertDto
                                                                          {
                                                                              SellerNumber = sellerNumber,
                                                                              Title        = "Title",
                                                                              Category     = "Category",
                                                                              Manufacturer = "Manufacturer",
                                                                              Price        = 1
                                                                          }));
            Assert.AreEqual("'SellerNumber' is not defined!", exception.Message);
        }

        [TestCase(null)]
        [TestCase("")]
        public void CreateAsync_BadTitle_ArgumentException(string value)
        {
            var exception = Assert.ThrowsAsync<ArgumentException>(async () =>
                                                                      await _articleLogic
                                                                          .CreateAsync(new ArticleInsertDto
                                                                          {
                                                                              SellerNumber = 1,
                                                                              Title        = value,
                                                                              Category     = "Category",
                                                                              Manufacturer = "Manufacturer",
                                                                              Price        = 1
                                                                          }));
            Assert.AreEqual("'Title' is not defined!", exception.Message);
        }

        [TestCase(null)]
        [TestCase("")]
        public void CreateAsync_BadCategory_ArgumentException(string value)
        {
            var exception = Assert.ThrowsAsync<ArgumentException>(async () =>
                                                                      await _articleLogic
                                                                          .CreateAsync(new ArticleInsertDto
                                                                          {
                                                                              SellerNumber = 1,
                                                                              Title        = "Title",
                                                                              Category     = value,
                                                                              Manufacturer = "Manufacturer",
                                                                              Price        = 1
                                                                          }));
            Assert.AreEqual("'Category' is not defined!", exception.Message);
        }

        [TestCase(null)]
        [TestCase("")]
        public void CreateAsync_BadManufacturer_ArgumentException(string value)
        {
            var exception = Assert.ThrowsAsync<ArgumentException>(async () =>
                                                                      await _articleLogic
                                                                          .CreateAsync(new ArticleInsertDto
                                                                          {
                                                                              SellerNumber = 1,
                                                                              Title        = "Title",
                                                                              Category     = "Category",
                                                                              Manufacturer = value,
                                                                              Price        = 1
                                                                          }));
            Assert.AreEqual("'Manufacturer' is not defined!", exception.Message);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-5)]
        [TestCase(-9)]
        public void CreateAsync_BadPrice_ArgumentException(double value)
        {
            var exception = Assert.ThrowsAsync<ArgumentException>(async () =>
                                                                      await _articleLogic
                                                                          .CreateAsync(new ArticleInsertDto
                                                                          {
                                                                              SellerNumber = 1,
                                                                              Title        = "Title",
                                                                              Category     = "Category",
                                                                              Manufacturer = "Manufacturer",
                                                                              Price        = value
                                                                          }));
            Assert.AreEqual("'Price' is not defined!", exception.Message);
        }

        [Test]
        public void CreateAsync_ArticleNumberExists_ArgumentException()
        {
            _articleRepository.GetItem(1).Returns(new ArticleModel());

            var exception = Assert.ThrowsAsync<ArgumentException>(async () =>
                                                                      await _articleLogic
                                                                          .CreateAsync(new ArticleInsertDto
                                                                          {
                                                                              Number       = 1,
                                                                              SellerNumber = 1,
                                                                              Title        = "Title",
                                                                              Category     = "Category",
                                                                              Manufacturer = "Manufacturer",
                                                                              Price        = 1
                                                                          }));
            Assert.AreEqual("Number '1' already in use!", exception.Message);
        }

        [Test]
        public void CreateAsync_SellerNumberNotExists_ArgumentException()
        {
            var exception = Assert.ThrowsAsync<ArgumentException>(async () =>
                                                                      await _articleLogic
                                                                          .CreateAsync(new ArticleInsertDto
                                                                          {
                                                                              Number       = 1,
                                                                              SellerNumber = 1,
                                                                              Title        = "Title",
                                                                              Category     = "Category",
                                                                              Manufacturer = "Manufacturer",
                                                                              Price        = 1
                                                                          }));
            Assert.AreEqual("SellerNumber '1' not known!", exception.Message);
        }


        [TestCase(1)]
        [TestCase(3)]
        public async Task CreateAsync_NoNumber_CreatedElementWidthAutoId(long expectedNumber)
        {
            _articleRepository.InsertAsync(Arg.Any<ArticleModel>()).Returns(info => info[0] is ArticleModel articleModel
                                                                                ? Task.FromResult(articleModel
                                                                                        .Number ==
                                                                                    expectedNumber &&
                                                                                    articleModel
                                                                                        .SellerNumber ==
                                                                                    1 &&
                                                                                    articleModel.Title ==
                                                                                    "Title" &&
                                                                                    articleModel
                                                                                        .Category ==
                                                                                    "Category" &&
                                                                                    articleModel
                                                                                        .Manufacturer ==
                                                                                    "Manufacturer" &&
                                                                                    Math.Abs(articleModel
                                                                                            .Price -
                                                                                        9.95d) <
                                                                                    0.000000001d &&
                                                                                    articleModel.SoldAt
                                                                                            .HasValue is
                                                                                        false &&
                                                                                    articleModel.SoldFor
                                                                                            .HasValue is
                                                                                        false &&
                                                                                    articleModel.ReturnedAt
                                                                                            .HasValue is
                                                                                        false &&
                                                                                    articleModel
                                                                                        .ReturnedTax
                                                                                        .HasValue is false)
                                                                                : Task.FromResult(false));
            _articleRepository.GetNextFreeNumber().Returns(expectedNumber);
            _sellerRepository.GetItem(1).Returns(new SellerModel());

            var item = new ArticleInsertDto
            {
                SellerNumber = 1,
                Title        = "Title",
                Category     = "Category",
                Manufacturer = "Manufacturer",
                Price        = 9.95d
            };
            var result = await _articleLogic.CreateAsync(item);

            Assert.IsTrue(result);
        }

        [TestCase(1)]
        [TestCase(3)]
        public async Task CreateAsync_Number_CreatedElementWidthAutoId(long expectedNumber)
        {
            _articleRepository.InsertAsync(Arg.Any<ArticleModel>()).Returns(info => info[0] is ArticleModel articleModel
                                                                                ? Task.FromResult(articleModel
                                                                                        .Number ==
                                                                                    expectedNumber &&
                                                                                    articleModel
                                                                                        .SellerNumber ==
                                                                                    1 &&
                                                                                    articleModel.Title ==
                                                                                    "Title" &&
                                                                                    articleModel
                                                                                        .Category ==
                                                                                    "Category" &&
                                                                                    articleModel
                                                                                        .Manufacturer ==
                                                                                    "Manufacturer" &&
                                                                                    Math.Abs(articleModel
                                                                                            .Price -
                                                                                        9.95d) <
                                                                                    0.000000001d &&
                                                                                    articleModel.SoldAt
                                                                                            .HasValue is
                                                                                        false &&
                                                                                    articleModel.SoldFor
                                                                                            .HasValue is
                                                                                        false &&
                                                                                    articleModel.ReturnedAt
                                                                                            .HasValue is
                                                                                        false &&
                                                                                    articleModel
                                                                                        .ReturnedTax
                                                                                        .HasValue is false)
                                                                                : Task.FromResult(false));
            _sellerRepository.GetItem(1).Returns(new SellerModel());

            var item = new ArticleInsertDto
            {
                Number       = expectedNumber,
                SellerNumber = 1,
                Title        = "Title",
                Category     = "Category",
                Manufacturer = "Manufacturer",
                Price        = 9.95d
            };
            var result = await _articleLogic.CreateAsync(item);

            Assert.IsTrue(result);
        }

        #endregion CreateAsync

        #region UpdateAsync

        [Test]
        public void UpdateAsync_InjectNull_ArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _articleLogic.UpdateAsync(null));
        }

        [Test]
        public void UpdateAsync_EmptyElement_ArgumentException()
        {
            Assert.ThrowsAsync<ArgumentException>(async () => await _articleLogic.UpdateAsync(new ArticleUpdateDto()));
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-5)]
        [TestCase(-9)]
        public void UpdateAsync_BadNumber_ArgumentException(long value)
        {
            var exception = Assert.ThrowsAsync<ArgumentException>(async () =>
                                                                      await _articleLogic
                                                                          .UpdateAsync(new ArticleUpdateDto
                                                                          {
                                                                              Number       = value,
                                                                              SellerNumber = 1,
                                                                              Title        = "Title",
                                                                              Category     = "Category",
                                                                              Manufacturer = "Manufacturer",
                                                                              Price        = 1
                                                                          }));
            Assert.AreEqual("'Number' is not defined!", exception.Message);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-5)]
        [TestCase(-9)]
        public void UpdateAsync_BadSellerNumber_ArgumentException(long value)
        {
            var exception = Assert.ThrowsAsync<ArgumentException>(async () =>
                                                                      await _articleLogic
                                                                          .UpdateAsync(new ArticleUpdateDto
                                                                          {
                                                                              Number       = 1,
                                                                              SellerNumber = value,
                                                                              Title        = "Title",
                                                                              Category     = "Category",
                                                                              Manufacturer = "Manufacturer",
                                                                              Price        = 1
                                                                          }));
            Assert.AreEqual("'SellerNumber' is not defined!", exception.Message);
        }

        [TestCase(null)]
        [TestCase("")]
        public void UpdateAsync_BadTitle_ArgumentException(string value)
        {
            var exception = Assert.ThrowsAsync<ArgumentException>(async () =>
                                                                      await _articleLogic
                                                                          .UpdateAsync(new ArticleUpdateDto
                                                                          {
                                                                              Number       = 1,
                                                                              SellerNumber = 1,
                                                                              Title        = value,
                                                                              Category     = "Category",
                                                                              Manufacturer = "Manufacturer",
                                                                              Price        = 1
                                                                          }));
            Assert.AreEqual("'Title' is not defined!", exception.Message);
        }

        [TestCase(null)]
        [TestCase("")]
        public void UpdateAsync_BadCategory_ArgumentException(string value)
        {
            var exception = Assert.ThrowsAsync<ArgumentException>(async () =>
                                                                      await _articleLogic
                                                                          .UpdateAsync(new ArticleUpdateDto
                                                                          {
                                                                              Number       = 1,
                                                                              SellerNumber = 1,
                                                                              Title        = "Title",
                                                                              Category     = value,
                                                                              Manufacturer = "Manufacturer",
                                                                              Price        = 1
                                                                          }));
            Assert.AreEqual("'Category' is not defined!", exception.Message);
        }

        [TestCase(null)]
        [TestCase("")]
        public void UpdateAsync_BadManufacturer_ArgumentException(string value)
        {
            var exception = Assert.ThrowsAsync<ArgumentException>(async () =>
                                                                      await _articleLogic
                                                                          .UpdateAsync(new ArticleUpdateDto
                                                                          {
                                                                              Number       = 1,
                                                                              SellerNumber = 1,
                                                                              Title        = "Title",
                                                                              Category     = "Category",
                                                                              Manufacturer = value,
                                                                              Price        = 1
                                                                          }));
            Assert.AreEqual("'Manufacturer' is not defined!", exception.Message);
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-5)]
        [TestCase(-9)]
        public void UpdateAsync_BadPrice_ArgumentException(double value)
        {
            var exception = Assert.ThrowsAsync<ArgumentException>(async () =>
                                                                      await _articleLogic
                                                                          .UpdateAsync(new ArticleUpdateDto
                                                                          {
                                                                              Number       = 1,
                                                                              SellerNumber = 1,
                                                                              Title        = "Title",
                                                                              Category     = "Category",
                                                                              Manufacturer = "Manufacturer",
                                                                              Price        = value
                                                                          }));
            Assert.AreEqual("'Price' is not defined!", exception.Message);
        }

        [Test]
        public void UpdateAsync_ArticleNumberNotExists_ArgumentException()
        {
            var exception = Assert.ThrowsAsync<ArgumentException>(async () =>
                                                                      await _articleLogic
                                                                          .UpdateAsync(new ArticleUpdateDto
                                                                          {
                                                                              Number       = 1,
                                                                              SellerNumber = 1,
                                                                              Title        = "Title",
                                                                              Category     = "Category",
                                                                              Manufacturer = "Manufacturer",
                                                                              Price        = 1
                                                                          }));
            Assert.AreEqual("Article '1' not known!", exception.Message);
        }

        [Test]
        public void UpdateAsync_SellerNumberNotExists_ArgumentException()
        {
            _articleRepository.GetItem(1).Returns(new ArticleModel());

            var exception = Assert.ThrowsAsync<ArgumentException>(async () =>
                                                                      await _articleLogic
                                                                          .UpdateAsync(new ArticleUpdateDto
                                                                          {
                                                                              Number       = 1,
                                                                              SellerNumber = 1,
                                                                              Title        = "Title",
                                                                              Category     = "Category",
                                                                              Manufacturer = "Manufacturer",
                                                                              Price        = 1
                                                                          }));
            Assert.AreEqual("SellerNumber '1' not known!", exception.Message);
        }

        [Test]
        public void UpdateAsync_AlreadySold_ArgumentException()
        {
            _articleRepository.GetItem(1).Returns(new ArticleModel{ SoldAt  = DateTime.Now});
            _sellerRepository.GetItem(1).Returns(new SellerModel());

            var exception = Assert.ThrowsAsync<ArgumentException>(async () =>
                                                                      await _articleLogic
                                                                          .UpdateAsync(new ArticleUpdateDto
                                                                          {
                                                                              Number       = 1,
                                                                              SellerNumber = 1,
                                                                              Title        = "Title",
                                                                              Category     = "Category",
                                                                              Manufacturer = "Manufacturer",
                                                                              Price        = 1
                                                                          }));
            Assert.AreEqual("Article '1' already sold!", exception.Message);
        }

        [Test]
        public void UpdateAsync_AlreadyReturned_ArgumentException()
        {
            _articleRepository.GetItem(1).Returns(new ArticleModel{ ReturnedAt = DateTime.Now});
            _sellerRepository.GetItem(1).Returns(new SellerModel());

            var exception = Assert.ThrowsAsync<ArgumentException>(async () =>
                                                                      await _articleLogic
                                                                          .UpdateAsync(new ArticleUpdateDto
                                                                          {
                                                                              Number       = 1,
                                                                              SellerNumber = 1,
                                                                              Title        = "Title",
                                                                              Category     = "Category",
                                                                              Manufacturer = "Manufacturer",
                                                                              Price        = 1
                                                                          }));
            Assert.AreEqual("Article '1' already returned!", exception.Message);
        }

        [Test]
        public async Task UpdateAsync_Number_CreatedElementWidthAutoId()
        {
            _articleRepository.GetItem(1).Returns(new ArticleModel {Number = 1});
            _sellerRepository.GetItem(1).Returns(new SellerModel());

            _articleRepository.UpdateAsync(Arg.Any<long>(), Arg.Any<ArticleModel>()).Returns(info => info[1] is ArticleModel articleModel
                                                                                                    ? Task.FromResult(articleModel.Number == 1 &&
                                                                                                        articleModel.SellerNumber == 1 &&
                                                                                                        articleModel.Title == "Title" &&
                                                                                                        articleModel.Category == "Category" &&
                                                                                                        articleModel.Manufacturer == "Manufacturer" &&
                                                                                                        Math.Abs(articleModel.Price - 9.95d) < 0.000000001d &&
                                                                                                        articleModel.SoldAt.HasValue is false &&
                                                                                                        articleModel.SoldFor.HasValue is false &&
                                                                                                        articleModel.ReturnedAt.HasValue is false &&
                                                                                                        articleModel.ReturnedTax.HasValue is false)
                                                                                                    : Task.FromResult(false));
            _sellerRepository.GetItem(1).Returns(new SellerModel());

            var item = new ArticleUpdateDto
            {
                Number       = 1,
                SellerNumber = 1,
                Title        = "Title",
                Category     = "Category",
                Manufacturer = "Manufacturer",
                Price        = 9.95d
            };
            var result = await _articleLogic.UpdateAsync(item);

            Assert.IsTrue(result);
        }

        #endregion UpdateAsync

        #region DeleteAsync

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-3)]
        [TestCase(-5)]
        public void DeleteAsync_InjectBadNumber_ArgumentNullException(long number)
        {
            var exception = Assert.ThrowsAsync<ArgumentException>(async () => await _articleLogic.DeleteAsync(number));
            Assert.AreEqual("'number' is not defined!", exception.Message);
        }

        [TestCase(1)]
        [TestCase(3)]
        [TestCase(5)]
        public async Task DeleteAsync_NotKnownElement_ArgumentException(long number)
        {
            _articleRepository.DeleteAsync(number).Returns(info => (long) info[0] == number);

            Assert.IsTrue(await _articleLogic.DeleteAsync(number));
        }

        #endregion DeleteAsync

        #region SetArticleOnMarkedAsync

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-5)]
        [TestCase(-9)]
        [TestCase(1)]
        [TestCase(5)]
        [TestCase(9)]
        public async Task SetArticleOnMarkedAsync_NotKnownArticleNumber_ArticleNotKnown(long value)
        {
            var result = await _articleLogic.SetArticleOnMarkedAsync(value, 1);
            Assert.AreEqual(value, result.ArticleNumber);
            Assert.AreEqual("ArticleNotKnown", result.ErrorCode);
        }

        [TestCase(-1)]
        [TestCase(-5)]
        [TestCase(-9)]
        public async Task SetArticleOnMarkedAsync_BadPrice_ArticlePriceNotValid(double value)
        {
            _articleRepository.GetItem(1).Returns(new ArticleModel());

            var result = await _articleLogic.SetArticleOnMarkedAsync(1, value);
            Assert.AreEqual(1, result.ArticleNumber);
            Assert.AreEqual("ArticlePriceNotValid", result.ErrorCode);
        }

        [Test]
        public async Task SetArticleOnMarkedAsync_ValidInput_Change()
        {
            _articleRepository.GetItem(1).Returns(new ArticleModel
            {
                Number       = 1,
                SellerNumber = 1,
                Title        = "Title",
                Category     = "Category",
                Manufacturer = "Manufacturer",
                OnSaleSince  = null,
                Price        = 4711,
                SoldAt       = DateTime.Now,
                SoldFor      = 1,
                ReturnedAt   = DateTime.Now,
                ReturnedTax  = 2
            });

            _articleRepository.UpdateAsync(Arg.Any<long>(), Arg.Any<ArticleModel>()).Returns(info => info[1] is ArticleModel articleModel
                ? Task.FromResult(articleModel.Number == 1 &&
                                  articleModel.SellerNumber == 1 &&
                                  articleModel.Title == "Title" &&
                                  articleModel.Category == "Category" &&
                                  articleModel.Manufacturer == "Manufacturer" &&
                                  Math.Abs(articleModel.Price - 9.95d) < 0.000000001d &&
                                  articleModel.OnSaleSince.HasValue &&
                                  articleModel.SoldAt.HasValue is false &&
                                  articleModel.SoldFor.HasValue is false &&
                                  articleModel.ReturnedAt.HasValue is false &&
                                  articleModel.ReturnedTax.HasValue is false)
                : Task.FromResult(false));

            var result = await _articleLogic.SetArticleOnMarkedAsync(1, 9.95d);
            Assert.AreEqual(1, result.ArticleNumber);
            Assert.IsNull(result.ErrorCode);
        }

        #endregion SetArticleOnMarkedAsync

        #region RemoveArticleFromMarkedAsync

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-5)]
        [TestCase(-9)]
        [TestCase(1)]
        [TestCase(5)]
        [TestCase(9)]
        public async Task RemoveArticleFromMarkedAsync_NotKnownArticleNumber_ArticleNotKnown(long value)
        {
            var result = await _articleLogic.RemoveArticleFromMarkedAsync(value);
            Assert.AreEqual(value, result.ArticleNumber);
            Assert.AreEqual("ArticleNotKnown", result.ErrorCode);
        }

        [Test]
        public async Task RemoveArticleFromMarkedAsync_SoldArticle_ArticleAlreadySold()
        {
            _articleRepository.GetItem(1).Returns(new ArticleModel {SoldAt = DateTime.Now});

            var result = await _articleLogic.RemoveArticleFromMarkedAsync(1);
            Assert.AreEqual(1, result.ArticleNumber);
            Assert.AreEqual("ArticleAlreadySold", result.ErrorCode);
        }

        [Test]
        public async Task RemoveArticleFromMarkedAsync_ReturnedArticle_ArticleAlreadyReturned()
        {
            _articleRepository.GetItem(1).Returns(new ArticleModel {ReturnedAt = DateTime.Now});

            var result = await _articleLogic.RemoveArticleFromMarkedAsync(1);
            Assert.AreEqual(1, result.ArticleNumber);
            Assert.AreEqual("ArticleAlreadyReturned", result.ErrorCode);
        }

        [Test]
        public async Task RemoveArticleFromMarkedAsync_ValidInput_Change()
        {
            _articleRepository.GetItem(1).Returns(new ArticleModel
            {
                Number       = 1,
                SellerNumber = 1,
                Title        = "Title",
                Category     = "Category",
                Manufacturer = "Manufacturer",
                OnSaleSince  = DateTime.Now,
                Price        = 4711,
                SoldAt       = null,
                SoldFor      = null,
                ReturnedAt   = null,
                ReturnedTax  = null
            });

            _articleRepository.UpdateAsync(Arg.Any<long>(), Arg.Any<ArticleModel>()).Returns(info => info[1] is ArticleModel articleModel
                ? Task.FromResult(articleModel.Number == 1 &&
                                  articleModel.SellerNumber == 1 &&
                                  articleModel.Title == "Title" &&
                                  articleModel.Category == "Category" &&
                                  articleModel.Manufacturer == "Manufacturer" &&
                                  Math.Abs(articleModel.Price - 4711) < 0.000000001d &&
                                  articleModel.OnSaleSince.HasValue is false &&
                                  articleModel.SoldAt.HasValue is false &&
                                  articleModel.SoldFor.HasValue is false &&
                                  articleModel.ReturnedAt.HasValue is false &&
                                  articleModel.ReturnedTax.HasValue is false)
                : Task.FromResult(false));

            var result = await _articleLogic.SetArticleOnMarkedAsync(1, 9.95d);
            Assert.AreEqual(1, result.ArticleNumber);
            Assert.IsNull(result.ErrorCode);
        }

        #endregion RemoveArticleFromMarkedAsync
    }
}
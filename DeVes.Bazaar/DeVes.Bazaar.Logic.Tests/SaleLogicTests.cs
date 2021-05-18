using System;
using System.Linq;
using System.Threading.Tasks;
using DeVes.Bazaar.Contracts.Models;
using DeVes.Bazaar.Contracts.Repositories;
using NSubstitute;
using NUnit.Framework;

namespace DeVes.Bazaar.Logic.Tests
{
    public class SaleLogicTests
    {
        private IArticleRepository _articleRepository;
        private SaleLogic          _saleLogic;


        [SetUp]
        public void Setup()
        {
            _articleRepository = Substitute.For<IArticleRepository>();
            _saleLogic         = new SaleLogic(_articleRepository);
        }


        #region Ctor

        [Test]
        public void Ctor_injectNull_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new SaleLogic(null));
        }

        [Test]
        public void Ctor_injectInstance_Instance()
        {
            Assert.NotNull(new SaleLogic(_articleRepository));
        }

        #endregion Ctor

        #region SellArticlesAsync

        [Test]
        public void SellArticlesAsync_Null_ArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(async () => await _saleLogic.SellArticlesAsync(null));
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(4)]
        [TestCase(8)]
        public async Task SellArticlesAsync_NotKnownArticleNumber_ArticleNotKnown(long number)
        {
            var result = await _saleLogic.SellArticlesAsync(new[] {number});

            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result.ArticleReceipts);
            Assert.AreEqual(1, result.ArticleReceipts.Count());
            Assert.AreEqual(0, result.TotalAmount);

            Assert.AreEqual(number, result.ArticleReceipts.First().ArticleNumber);
            Assert.IsNull(result.ArticleReceipts.First().Price);
            Assert.AreEqual("ArticleNotKnown", result.ArticleReceipts.First().ErrorCode);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(4)]
        [TestCase(8)]
        public async Task SellArticlesAsync_KnownArticleNotOnSale_ArticleNotFreeForSale(long number)
        {
            _articleRepository.GetItem(number).Returns(new ArticleModel
            {
                Number      = number,
                OnSaleSince = null,
                SoldAt      = null,
                ReturnedAt  = null
            });


            var result = await _saleLogic.SellArticlesAsync(new[] {number});

            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result.ArticleReceipts);
            Assert.AreEqual(1, result.ArticleReceipts.Count());
            Assert.AreEqual(0, result.TotalAmount);

            Assert.AreEqual(number, result.ArticleReceipts.First().ArticleNumber);
            Assert.IsNull(result.ArticleReceipts.First().Price);
            Assert.AreEqual("ArticleNotFreeForSale", result.ArticleReceipts.First().ErrorCode);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(4)]
        [TestCase(8)]
        public async Task SellArticlesAsync_KnownArticleButSold_ArticleAlreadySold(long number)
        {
            _articleRepository.GetItem(number).Returns(new ArticleModel
            {
                Number      = number,
                OnSaleSince = DateTime.Now,
                SoldAt      = DateTime.Now,
                ReturnedAt  = null
            });


            var result = await _saleLogic.SellArticlesAsync(new[] { number });

            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result.ArticleReceipts);
            Assert.AreEqual(1, result.ArticleReceipts.Count());
            Assert.AreEqual(0, result.TotalAmount);

            Assert.AreEqual(number, result.ArticleReceipts.First().ArticleNumber);
            Assert.IsNull(result.ArticleReceipts.First().Price);
            Assert.AreEqual("ArticleAlreadySold", result.ArticleReceipts.First().ErrorCode);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(4)]
        [TestCase(8)]
        public async Task SellArticlesAsync_KnownArticleButReturned_ArticleAlreadyReturned(long number)
        {
            _articleRepository.GetItem(number).Returns(new ArticleModel
            {
                Number      = number,
                OnSaleSince = DateTime.Now,
                SoldAt      = null,
                ReturnedAt  = DateTime.Now
            });


            var result = await _saleLogic.SellArticlesAsync(new[] { number });

            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result.ArticleReceipts);
            Assert.AreEqual(1, result.ArticleReceipts.Count());
            Assert.AreEqual(0, result.TotalAmount);

            Assert.AreEqual(number, result.ArticleReceipts.First().ArticleNumber);
            Assert.IsNull(result.ArticleReceipts.First().Price);
            Assert.AreEqual("ArticleAlreadyReturned", result.ArticleReceipts.First().ErrorCode);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(4)]
        [TestCase(8)]
        public async Task SellArticlesAsync_ValidArticle_UpdateDb(long number)
        {
            _articleRepository.GetItem(number).Returns(new ArticleModel
            {
                Number      = number,
                OnSaleSince = DateTime.Now,
                SoldAt      = null,
                ReturnedAt  = null,
                Price       = 10
            });


            var updateCounter = 0;
            var totalPrice    = 0.0d;
            _articleRepository.When(x => x.UpdateAsync(number, Arg.Any<ArticleModel>()))
                              .Do(x =>
                              {
                                  if (!(x[1] is ArticleModel articleModel)) return;
                                  if (articleModel.SoldAt.HasValue is false) return;
                                  if (articleModel.SoldFor.HasValue is false) return;

                                  updateCounter++;
                                  totalPrice += articleModel.SoldFor.Value;
                              });


            var result = await _saleLogic.SellArticlesAsync(new[] { number });

            Assert.IsNotNull(result);
            Assert.IsNotEmpty(result.ArticleReceipts);
            Assert.AreEqual(1, result.ArticleReceipts.Count());
            Assert.AreEqual(totalPrice, result.TotalAmount);

            Assert.AreEqual(number, result.ArticleReceipts.First().ArticleNumber);
            Assert.AreEqual(10, result.ArticleReceipts.First().Price);
            Assert.IsNull(result.ArticleReceipts.First().ErrorCode);

            Assert.AreEqual(1, updateCounter);
        }

        #endregion SellArticlesAsync
    }
}
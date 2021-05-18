using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeVes.Bazaar.Contracts.Dto;
using DeVes.Bazaar.Contracts.Logic;
using DeVes.Bazaar.Contracts.Models;
using DeVes.Bazaar.Contracts.Repositories;

namespace DeVes.Bazaar.Logic
{
    public class ArticleLogic : IArticleLogic
    {
        private readonly IArticleRepository _articleRepository;
        private readonly ISellerRepository  _sellerRepository;


        public ArticleLogic(IArticleRepository articleRepository, ISellerRepository sellerRepository)
        {
            _articleRepository = articleRepository ?? throw new ArgumentNullException(nameof(articleRepository));
            _sellerRepository  = sellerRepository ?? throw new ArgumentNullException(nameof(sellerRepository));
        }


        public IEnumerable<ArticleModel> GetItems(long?  number, long?  sellerNumber,
                                                  string title,  string category, string manufacturer,
                                                  bool?  isSold, bool?  isReturned)
        {
            return _articleRepository.GetItems(number, sellerNumber, title, category, manufacturer, isSold, isReturned);
        }

        public ArticleModel GetItem(long number) => _articleRepository.GetItem(number);


        public async Task<bool> CreateAsync(ArticleInsertDto value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            if (value.SellerNumber <= 0)
                throw new ArgumentException($"'{nameof(value.SellerNumber)}' is not defined!");
            if (string.IsNullOrWhiteSpace(value.Title))
                throw new ArgumentException($"'{nameof(value.Title)}' is not defined!");
            if (string.IsNullOrWhiteSpace(value.Category))
                throw new ArgumentException($"'{nameof(value.Category)}' is not defined!");
            if (string.IsNullOrWhiteSpace(value.Manufacturer))
                throw new ArgumentException($"'{nameof(value.Manufacturer)}' is not defined!");
            if (value.Price <= 0)
                throw new ArgumentException($"'{nameof(value.Price)}' is not defined!");

            value.Number = value.Number <= 0
                               ? _articleRepository.GetNextFreeNumber()
                               : value.Number;

            if (_articleRepository.GetItem(value.Number) != null)
                throw new ArgumentException($"Number '{value.Number}' already in use!");
            if (_sellerRepository.GetItem(value.SellerNumber) == null)
                throw new ArgumentException($"SellerNumber '{value.Number}' not known!");

            var articleModel = new ArticleModel
            {
                Number       = value.Number,
                SellerNumber = value.SellerNumber,
                Title        = value.Title,
                Category     = value.Category,
                Manufacturer = value.Manufacturer,
                Price        = value.Price
            };

            return await _articleRepository.InsertAsync(articleModel);
        }

        public async Task<bool> UpdateAsync(ArticleUpdateDto value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            if (value.Number <= 0)
                throw new ArgumentException($"'{nameof(value.Number)}' is not defined!");
            if (value.SellerNumber <= 0)
                throw new ArgumentException($"'{nameof(value.SellerNumber)}' is not defined!");
            if (string.IsNullOrWhiteSpace(value.Title))
                throw new ArgumentException($"'{nameof(value.Title)}' is not defined!");
            if (string.IsNullOrWhiteSpace(value.Category))
                throw new ArgumentException($"'{nameof(value.Category)}' is not defined!");
            if (string.IsNullOrWhiteSpace(value.Manufacturer))
                throw new ArgumentException($"'{nameof(value.Manufacturer)}' is not defined!");
            if (value.Price <= 0) throw new ArgumentException($"'{nameof(value.Price)}' is not defined!");

            var articleModel = _articleRepository.GetItem(value.Number);

            if (articleModel == null)
                throw new ArgumentException($"Article '{value.Number}' not known!");
            if (_sellerRepository.GetItem(value.SellerNumber ?? articleModel.SellerNumber) == null)
                throw new ArgumentException($"SellerNumber '{value.Number}' not known!");
            if (articleModel.SoldAt.HasValue)
                throw new ArgumentException($"Article '{value.Number}' already sold!");
            if (articleModel.ReturnedAt.HasValue)
                throw new ArgumentException($"Article '{value.Number}' already returned!");

            articleModel.SellerNumber = value.SellerNumber ?? articleModel.SellerNumber;
            articleModel.Title        = value.Title ?? articleModel.Title;
            articleModel.Category     = value.Category ?? articleModel.Category;
            articleModel.Manufacturer = value.Manufacturer ?? articleModel.Manufacturer;
            articleModel.Price        = value.Price ?? articleModel.Price;

            return await _articleRepository.UpdateAsync(articleModel.Number, articleModel);
        }

        public async Task<bool> DeleteAsync(long number)
        {
            if (number <= 0) throw new ArgumentException($"'{nameof(number)}' is not defined!");

            return await _articleRepository.DeleteAsync(number);
        }


        public async Task<MarkedResponseDto> SetArticleOnMarkedAsync(long articleNumber, double? price)
        {
            var articleElement = _articleRepository.GetItem(articleNumber);

            if (articleElement == null)
                return MarkedResponseDto.Create(articleNumber, ErrorCodes.ArticleNotKnown);
            if (price.HasValue && price < 0)
                return MarkedResponseDto.Create(articleNumber, ErrorCodes.ArticlePriceNotValid);

            articleElement.OnSaleSince ??= DateTime.Now;
            articleElement.Price       =   price ?? articleElement.Price;

            articleElement.SoldAt      = null;
            articleElement.SoldFor     = null;
            articleElement.ReturnedAt  = null;
            articleElement.ReturnedTax = null;

            await _articleRepository.UpdateAsync(articleElement.Number, articleElement);

            return MarkedResponseDto.Create(articleNumber);
        }

        public async Task<MarkedResponseDto> RemoveArticleFromMarkedAsync(long articleNumber)
        {
            var articleElement = _articleRepository.GetItem(articleNumber);

            if (articleElement == null)
                return MarkedResponseDto.Create(articleNumber, ErrorCodes.ArticleNotKnown);
            if (articleElement.SoldAt.HasValue)
                return MarkedResponseDto.Create(articleNumber, ErrorCodes.ArticleAlreadySold);
            if (articleElement.ReturnedAt.HasValue)
                return MarkedResponseDto.Create(articleNumber, ErrorCodes.ArticleAlreadyReturned);

            articleElement.OnSaleSince = null;

            await _articleRepository.UpdateAsync(articleElement.Number, articleElement);

            return MarkedResponseDto.Create(articleNumber);
        }
    }
}
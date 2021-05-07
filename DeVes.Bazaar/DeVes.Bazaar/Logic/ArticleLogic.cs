using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeVes.Bazaar.Data.Contracts.Logic;
using DeVes.Bazaar.Data.Contracts.Models;
using DeVes.Bazaar.Data.Contracts.Repositories;

namespace DeVes.Bazaar.Logic
{
    public class ArticleLogic : IArticleLogic
    {
        private readonly IArticleRepository _articleRepository;
        private readonly ISellerRepository _sellerRepository;


        public ArticleLogic(IArticleRepository articleRepository, ISellerRepository sellerRepository)
        {
            _articleRepository = articleRepository ?? throw new ArgumentNullException(nameof(articleRepository));
            _sellerRepository = sellerRepository ?? throw new ArgumentNullException(nameof(sellerRepository));
        }


        public IEnumerable<ArticleModel> GetItems() => _articleRepository.GetItems();
        public ArticleModel GetItem(long number) => _articleRepository.GetItem(number);
        public IEnumerable<ArticleModel> GetItemsOfSeller(long sellerNumber) => _articleRepository.GetItemsOfSeller(sellerNumber);

        
        public async Task<bool> CreateAsync(ArticleModel value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            if (value.SellerNumber <= 0) throw new ArgumentException($"'{nameof(value.SellerNumber)}' is not defined!");
            if (string.IsNullOrWhiteSpace(value.Title)) throw new ArgumentException($"'{nameof(value.Title)}' is not defined!");
            if (string.IsNullOrWhiteSpace(value.Category)) throw new ArgumentException($"'{nameof(value.Category)}' is not defined!");
            if (string.IsNullOrWhiteSpace(value.Manufacturer)) throw new ArgumentException($"'{nameof(value.Manufacturer)}' is not defined!");
            if (value.Price <= 0) throw new ArgumentException($"'{nameof(value.Price)}' is not defined!");

            if (value.SoldAt.HasValue || value.SoldFor.HasValue)
            {
                if (value.OnSaleSince.HasValue is false) throw new ArgumentException($"'{nameof(value.OnSaleSince)}' is not defined!");
                if (value.SoldAt.HasValue is false) throw new ArgumentException($"'{nameof(value.SoldAt)}' is not defined!");
                if (value.SoldFor.HasValue is false) throw new ArgumentException($"'{nameof(value.SoldFor)}' is not defined!");
                if (value.SoldFor <= 0) throw new ArgumentException($"'{nameof(value.SoldFor)}' is not defined!");
            }

            value.Number = value.Number <= 0
                ? _articleRepository.GetNextFreeNumber()
                : value.Number;

            if (_articleRepository.GetItem(value.Number) != null) throw new ArgumentException($"Number '{value.Number}' already in use!");
            if (_sellerRepository.GetItem(value.SellerNumber) == null) throw new ArgumentException($"SellerNumber '{value.Number}' not known!");

            return await _articleRepository.InsertAsync(value);
        }

        public async Task<bool> UpdateAsync(ArticleModel value)
        {
            if (value.Number <= 0) throw new ArgumentException($"'{nameof(value.Number)}' is not defined!");
            if (value.SellerNumber <= 0) throw new ArgumentException($"'{nameof(value.SellerNumber)}' is not defined!");
            if (string.IsNullOrWhiteSpace(value.Title)) throw new ArgumentException($"'{nameof(value.Title)}' is not defined!");
            if (string.IsNullOrWhiteSpace(value.Category)) throw new ArgumentException($"'{nameof(value.Category)}' is not defined!");
            if (string.IsNullOrWhiteSpace(value.Manufacturer)) throw new ArgumentException($"'{nameof(value.Manufacturer)}' is not defined!");
            if (value.Price <= 0) throw new ArgumentException($"'{nameof(value.Price)}' is not defined!");

            if (value.SoldAt.HasValue || value.SoldFor.HasValue)
            {
                if (value.SoldAt.HasValue is false) throw new ArgumentException($"'{nameof(value.SoldAt)}' is not defined!");
                if (value.SoldFor.HasValue is false) throw new ArgumentException($"'{nameof(value.SoldFor)}' is not defined!");
                if (value.SoldFor <= 0) throw new ArgumentException($"'{nameof(value.SoldFor)}' is not defined!");
            }

            if (_articleRepository.GetItem(value.Number) == null) throw new ArgumentException($"Article {value.Number} not known!");
            if (_sellerRepository.GetItem(value.SellerNumber) == null) throw new ArgumentException($"SellerNumber '{value.Number}' not known!");

            return await _articleRepository.UpdateAsync(value.SellerNumber, value);
        }

        public async Task<bool> DeleteAsync(long number)
        {
            if (number <= 0) throw new ArgumentException($"'{nameof(number)}' is not defined!");

            return await _articleRepository.DeleteAsync(number);
        }
    }
}
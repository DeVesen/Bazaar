using System;
using System.Collections.Generic;
using System.Linq;
using DeVes.Bazaar.Contracts.Models;
using DeVes.Bazaar.Contracts.Repositories;

namespace DeVes.Bazaar.Data.JsonDb
{
    public class ArticleRepository : BaseRepository<ArticleModel>, IArticleRepository
    {
        public ArticleRepository(RepoOptions repoOptions) : base(repoOptions)
        {
        }

        public IEnumerable<ArticleModel> GetItems(long?  number       = null, long?  sellerNumber = null,
                                                  string title        = null, string category     = null,
                                                  string manufacturer = null,
                                                  bool?  isSold       = null, bool? isReturned = null)
        {
            var result = Collection.AsQueryable();

            if (number.HasValue)
                result = result.Where(p => p.Number == number);
            if (sellerNumber.HasValue)
                result = result.Where(p => p.SellerNumber == sellerNumber);

            if (string.IsNullOrWhiteSpace(title) is false)
                result = result.Where(p => p.Title != null &&
                                           p.Title.ToLower()
                                            .Contains(title.ToLower(), StringComparison.OrdinalIgnoreCase));
            if (string.IsNullOrWhiteSpace(category) is false)
                result = result.Where(p => p.Category != null &&
                                           p.Category.ToLower()
                                            .Contains(category.ToLower(), StringComparison.OrdinalIgnoreCase));
            if (string.IsNullOrWhiteSpace(manufacturer) is false)
                result = result.Where(p => p.Manufacturer != null && p.Manufacturer.ToLower()
                                                                      .Contains(manufacturer.ToLower(),
                                                                                    StringComparison
                                                                                        .OrdinalIgnoreCase));

            if (isSold.HasValue)
                result = result.Where(p => p.SoldAt.HasValue == isSold);
            if (isReturned.HasValue)
                result = result.Where(p => p.ReturnedAt.HasValue == isReturned);

            return result;
        }
    }
}
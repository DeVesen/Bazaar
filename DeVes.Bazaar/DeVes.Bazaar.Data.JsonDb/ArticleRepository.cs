using System.Collections.Generic;
using System.Linq;
using DeVes.Bazaar.Data.Contracts.Models;
using DeVes.Bazaar.Data.Contracts.Repositories;
using JsonFlatFileDataStore;

namespace DeVes.Bazaar.Data.JsonDb
{
    public class ArticleRepository : BaseRepository<ArticleModel>, IArticleRepository
    {
        public ArticleRepository(RepoOptions repoOptions) : base(repoOptions)
        {
        }

        public IEnumerable<ArticleModel> GetItemsOfSeller(long sellerNumber) =>
            Collection.AsQueryable().Where(p => p.SellerNumber == sellerNumber);
    }
}

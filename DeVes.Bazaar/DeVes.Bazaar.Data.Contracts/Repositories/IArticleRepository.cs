using System.Collections.Generic;
using DeVes.Bazaar.Data.Contracts.Models;

namespace DeVes.Bazaar.Data.Contracts.Repositories
{
    public interface IArticleRepository : IBaseRepository<ArticleModel>
    {
        IEnumerable<ArticleModel> GetItemsOfSeller(long sellerNumber);
    }
}
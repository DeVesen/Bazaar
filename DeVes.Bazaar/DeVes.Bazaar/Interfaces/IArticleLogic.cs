using System.Collections.Generic;
using System.Threading.Tasks;
using DeVes.Bazaar.Data.Contracts.Models;

namespace DeVes.Bazaar.Interfaces
{
    public interface IArticleLogic
    {
        ArticleModel GetItem(long number);

        IEnumerable<ArticleModel> GetItems();
        IEnumerable<ArticleModel> GetItemsOfSeller(long sellerNumber);
        
        Task<bool> CreateAsync(ArticleModel value);
        Task<bool> UpdateAsync(ArticleModel value);
        Task<bool> DeleteAsync(long number);
    }
}
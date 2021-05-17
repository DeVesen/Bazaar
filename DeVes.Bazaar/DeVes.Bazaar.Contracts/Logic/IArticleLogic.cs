using System.Collections.Generic;
using System.Threading.Tasks;
using DeVes.Bazaar.Contracts.Dto;
using DeVes.Bazaar.Contracts.Models;

namespace DeVes.Bazaar.Contracts.Logic
{
    public interface IArticleLogic
    {
        ArticleModel GetItem(long number);

        IEnumerable<ArticleModel> GetItems(long?  number = null, long?  sellerNumber = null,
                                           string title  = null, string category     = null, string manufacturer = null,
                                           bool?  isSold = null, bool?  isReturned   = null);

        Task<bool> CreateAsync(ArticleInsertDto value);
        Task<bool> UpdateAsync(ArticleUpdateDto value);
        Task<bool> DeleteAsync(long               number);
    }
}
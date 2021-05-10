using System.Collections.Generic;
using System.Threading.Tasks;
using DeVes.Bazaar.Contracts.Models;

namespace DeVes.Bazaar.Contracts.Repositories
{
    public interface IArticleRepository
    {
        IEnumerable<ArticleModel> GetItems(long? number = null, long? sellerNumber = null,
                                           string title = null, string category = null, string manufacturer = null,
                                           bool? isSold = null, bool? isReturned = null);
        ArticleModel GetItem(long number);

        long GetNextFreeNumber();

        Task<bool> InsertAsync(ArticleModel value);
        Task<bool> UpdateAsync(long number, ArticleModel value);
        Task<bool> DeleteAsync(long number);
    }
}
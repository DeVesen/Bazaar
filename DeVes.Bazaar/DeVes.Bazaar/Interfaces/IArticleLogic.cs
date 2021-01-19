using System.Collections.Generic;
using DeVes.Bazaar.Data.Contracts.Models;

namespace DeVes.Bazaar.Interfaces
{
    public interface IArticleLogic
    {
        IEnumerable<ArticleModel> GetItems();
        IEnumerable<ArticleModel> GetItems(long? sellerNumber);
        ArticleModel GetItem(long number);
        void Create(ArticleModel value);
        void Update(ArticleModel value);
        void Delete(long number);
    }
}
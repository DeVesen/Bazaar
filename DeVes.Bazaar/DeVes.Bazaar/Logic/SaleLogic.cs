using System;
using System.Collections.Generic;
using System.Linq;
using DeVes.Bazaar.Dto;
using DeVes.Bazaar.Interfaces;

namespace DeVes.Bazaar.Logic
{
    public class SaleLogic : ISaleLogic
    {
        private readonly IArticleLogic _articleLogic;


        public SaleLogic(IArticleLogic articleLogic)
        {
            _articleLogic = articleLogic ?? throw new ArgumentNullException(nameof(articleLogic));
        }


        public void Sale(IEnumerable<SaleDto> salePositions)
        {
            if (salePositions == null) throw new ArgumentNullException(nameof(salePositions));

            var soldAtValue = DateTime.Now;

            var updateArticles = salePositions.Where(p => p != null)
                .Select((position, positionIndex) =>
                {
                    if (position.ArticleNumber <= 0) throw new ArgumentException($"Position {positionIndex + 1}: '{nameof(position.ArticleNumber)}' is not defined!");
                    if (position.SaleFor <= 0) throw new ArgumentException($"Position {positionIndex + 1}: '{nameof(position.SaleFor)}' is not defined!");

                    var articleItem = _articleLogic.GetItem(position.ArticleNumber);
                    if (articleItem == null) throw new ArgumentException($"Position {positionIndex + 1}: Article '{position.ArticleNumber}' not known!");

                    if (articleItem.OnSaleSince.HasValue is false) throw new ArgumentException($"Position {positionIndex + 1}: Article '{position.ArticleNumber}' is not released for sale!");
                    if (articleItem.SoldAt.HasValue) throw new ArgumentException($"Position {positionIndex + 1}: Article '{position.ArticleNumber}' is already sold!");
                    if (articleItem.SoldFor.HasValue) throw new ArgumentException($"Position {positionIndex + 1}: Article '{position.ArticleNumber}' is already sold!");
                    if (articleItem.ReturnedAt.HasValue) throw new ArgumentException($"Position {positionIndex + 1}: Article '{position.ArticleNumber}' is already returned to Seller!");

                    articleItem.SoldAt = soldAtValue;
                    articleItem.SoldFor = position.SaleFor;

                    return articleItem;
                }).ToArray();

            foreach (var updateArticle in updateArticles)
            {
                _articleLogic.Update(updateArticle);
            }
        }
    }
}
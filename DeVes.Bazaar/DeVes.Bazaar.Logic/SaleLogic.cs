using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeVes.Bazaar.Contracts.Dto;
using DeVes.Bazaar.Contracts.Logic;
using DeVes.Bazaar.Contracts.Repositories;

namespace DeVes.Bazaar.Logic
{
    public class SaleLogic : ISaleLogic
    {
        private readonly IArticleRepository _articleRepository;


        public SaleLogic(IArticleRepository articleRepository)
        {
            _articleRepository = articleRepository ?? throw new ArgumentNullException(nameof(articleRepository));
        }


        public async Task<SalesReceiptDto> SellArticlesAsync(IEnumerable<long> articleNumbers)
        {
            var allArticles      = articleNumbers.Select(p => new { ArticleNumber = p, Article = _articleRepository.GetItem(p)})
                                                 .ToArray();
            var resultReceiptLst = new List<ArticleReceiptDto>();

            foreach (var articleElement in allArticles)
            {
                if (articleElement.Article == null)
                {
                    resultReceiptLst.Add(ArticleReceiptDto.Create(articleElement.ArticleNumber, ErrorCodes.ArticleNotKnown));
                }
                else
                {
                    if (articleElement.Article.OnSaleSince.HasValue is false)
                        resultReceiptLst.Add(ArticleReceiptDto.Create(articleElement.ArticleNumber, ErrorCodes.ArticleNotFreeForSale));
                    if (articleElement.Article.SoldAt.HasValue)
                        resultReceiptLst.Add(ArticleReceiptDto.Create(articleElement.ArticleNumber, ErrorCodes.ArticleAlreadySold));
                    if (articleElement.Article.ReturnedAt.HasValue)
                        resultReceiptLst.Add(ArticleReceiptDto.Create(articleElement.ArticleNumber, ErrorCodes.ArticleAlreadyReturned));

                    articleElement.Article.SoldAt  = DateTime.Now;
                    articleElement.Article.SoldFor = articleElement.Article.Price;
                }
            }

            if (resultReceiptLst.Any()) return new SalesReceiptDto(resultReceiptLst.ToArray());

            foreach (var article in allArticles)
            {
                await _articleRepository.UpdateAsync(article.Article.Number, article.Article);
            }

            return new SalesReceiptDto(allArticles.Select(p => ArticleReceiptDto.Create(p.Article.Number, p.Article.SoldFor ?? 0)).ToArray());
        }
    }
}
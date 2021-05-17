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
            _articleRepository = articleRepository ?? throw new ArgumentNullException(nameof(_articleRepository));
        }


        public async Task<OnSaleResponseDto> SetOnSaleAsync(long articleNumber, double price)
        {
            var articleElement = _articleRepository.GetItem(articleNumber);

            if (articleElement == null) return OnSaleResponseDto.Create(articleNumber, ErrorCodes.ArticleNotKnown);
            if (articleElement.ReturnedAt.HasValue) return OnSaleResponseDto.Create(articleNumber, ErrorCodes.ArticleAlreadyReturned);
            if (price < 0) return OnSaleResponseDto.Create(articleNumber, ErrorCodes.ArticlePriceNotValid);

            articleElement.OnSaleSince ??= DateTime.Now;
            articleElement.Price       = price;

            await _articleRepository.UpdateAsync(articleElement.Number, articleElement);

            return OnSaleResponseDto.Create(articleNumber);
        }

        public async Task<SalesReceiptDto> SellItemsAsync(IEnumerable<long> articleNumbers)
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

        public async Task<RecallsSaleResponseDto> RecallsSaleAsync(long articleNumber)
        {
            var articleElement = _articleRepository.GetItem(articleNumber);

            if (articleElement == null) return RecallsSaleResponseDto.Create(articleNumber, ErrorCodes.ArticleNotKnown);
            if (articleElement.OnSaleSince.HasValue is false) return RecallsSaleResponseDto.Create(articleNumber, ErrorCodes.ArticleNotFreeForSale);
            if (articleElement.ReturnedAt.HasValue) return RecallsSaleResponseDto.Create(articleNumber, ErrorCodes.ArticleAlreadyReturned);

            articleElement.SoldAt      =   null;
            articleElement.SoldFor     =   null;

            await _articleRepository.UpdateAsync(articleElement.Number, articleElement);

            return RecallsSaleResponseDto.Create(articleNumber);
        }
    }
}
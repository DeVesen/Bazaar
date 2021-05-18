using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeVes.Bazaar.Contracts.Dto;
using DeVes.Bazaar.Contracts.Logic;
using DeVes.Bazaar.Contracts.Repositories;

namespace DeVes.Bazaar.Logic
{
    public class SellerBillingLogic : ISellerBillingLogic
    {
        private readonly ISellerRepository  _sellerRepository;
        private readonly IArticleRepository _articleRepository;


        public SellerBillingLogic(ISellerRepository  sellerRepository,
                                  IArticleRepository articleRepository)
        {
            _sellerRepository  = sellerRepository ?? throw new ArgumentNullException(nameof(sellerRepository));
            _articleRepository = articleRepository ?? throw new ArgumentNullException(nameof(articleRepository));
        }


        public async Task<BillingResponseDto> BillingAsync(long sellerNumber, IEnumerable<long> articleNumbers = null)
        {
            var billingTime    = DateTime.Now;
            var sellerElement  = _sellerRepository.GetItem(sellerNumber);
            var articlesToBill = _articleRepository.GetItems(null, sellerNumber)
                                                   .Where(p => (articleNumbers ?? new long[0]).Any() is false ||
                                                               (articleNumbers ?? new long[0]).Contains(p.Number))
                                                   .Where(p => p.ReturnedAt.HasValue is false)
                                                   .ToArray();

            if (sellerElement == null)         return new BillingResponseDto(sellerNumber, ErrorCodes.SellerNotKnown);
            if (articlesToBill.Any() is false) return new BillingResponseDto(sellerNumber, ErrorCodes.SellerNoUnBilledArticle);

            foreach (var article in articlesToBill)
            {
                article.ReturnedAt  = billingTime;
                article.ReturnedTax = article.SoldFor * sellerElement.TaxPercent / 100;
                await _articleRepository.UpdateAsync(article.Number, article);
            }

            var turnover = articlesToBill.Sum(p => p.SoldFor ?? 0);
            var tax      = articlesToBill.Sum(p => p.ReturnedTax ?? 0);

            return new BillingResponseDto(sellerNumber,
                                          billingTime,
                                          turnover,
                                          tax,
                                          articlesToBill.Select(p => p.Number).ToArray());
        }

        public async Task<CancelSettlementResponseDto> CancelSettlementAsync(long articleNumber)
        {
            var articleElement = _articleRepository.GetItem(articleNumber);

            if (articleElement == null) return CancelSettlementResponseDto.Create(articleNumber, ErrorCodes.ArticleNotKnown);

            articleElement.ReturnedAt  = null;
            articleElement.ReturnedTax = null;

            await _articleRepository.UpdateAsync(articleElement.Number, articleElement);

            return CancelSettlementResponseDto.Create(articleNumber);
        }
    }
}
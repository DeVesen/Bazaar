using System;
using System.Linq;
using DeVes.Bazaar.Dto;
using DeVes.Bazaar.Interfaces;
using Microsoft.Extensions.Configuration;

namespace DeVes.Bazaar.Logic
{
    public class AccountingLogic : IAccountingLogic
    {
        private readonly IConfiguration _configuration;
        private readonly IArticleLogic _articleLogic;


        public AccountingLogic(IConfiguration configuration, IArticleLogic articleLogic)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _articleLogic  = articleLogic  ?? throw new ArgumentNullException(nameof(articleLogic));
        }


        public AccountingListDto GetAccountingList(long sellerNumber)
        {
            var tax = _configuration.GetValue<double?>("Tax") ?? 0;

            var articles  = _articleLogic.GetItems(sellerNumber).ToArray();
            var turnover  = articles.Where(p => p.SoldFor.HasValue).Sum(p => p.SoldFor.Value);
            var taxMoveIn = turnover * tax / 100.0d;

            return new AccountingListDto
            {
                SellerNumber  = sellerNumber,
                TurnoverGross = turnover,
                TurnoverNet   = turnover - taxMoveIn
            };
        }

        public void ReturnRemainingArticles(long sellerNumber)
        {
            var returnedAtValue = DateTime.Now;

            var articles = _articleLogic.GetItems(sellerNumber)
                .Where(p => p.ReturnedAt.HasValue is false)
                    .Select(p =>
                    {
                        p.ReturnedAt = returnedAtValue;
                        return p;
                    });

            foreach (var updateArticle in articles)
            {
                _articleLogic.Update(updateArticle);
            }
        }
    }
}
using System.Collections.Generic;
using System.Linq;

namespace DeVes.Bazaar.Contracts.Dto
{
    public class SalesReceiptDto
    {
        public double                         TotalAmount     { get; }
        public IEnumerable<ArticleReceiptDto> ArticleReceipts { get; }

        public SalesReceiptDto(ArticleReceiptDto[] articleReceipts)
        {
            TotalAmount     = articleReceipts.Sum(p => p.Price ?? 0);
            ArticleReceipts = articleReceipts;
        }
    }
}
using System;
using System.Collections.Generic;

namespace DeVes.Bazaar.Contracts.Dto
{
    public class BillingResponseDto
    {
        public long              SellerNumber     { get; }
        public DateTime          CreationTime     { get; }
        public double            Turnover         { get; }
        public double            Tax              { get; }
        public IEnumerable<long> NowBilledArticle { get; }
        public string            ErrorCode        { get; }

        public BillingResponseDto(long   sellerNumber,
                                  string errorCode)
            : this(sellerNumber, DateTime.Now, 0, 0, null, errorCode)
        {
        }

        public BillingResponseDto(long              sellerNumber,
                                  DateTime          creationTime,
                                  double            turnover,
                                  double            tax,
                                  IEnumerable<long> nowBilledArticle,
                                  string            errorCode = null)
        {
            SellerNumber     = sellerNumber;
            CreationTime     = creationTime;
            Turnover         = turnover;
            Tax              = tax;
            NowBilledArticle = nowBilledArticle;
            ErrorCode        = errorCode;
        }
    }
}
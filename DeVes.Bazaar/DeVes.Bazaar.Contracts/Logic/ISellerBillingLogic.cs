using System.Collections.Generic;
using System.Threading.Tasks;
using DeVes.Bazaar.Contracts.Dto;

namespace DeVes.Bazaar.Contracts.Logic
{
    public interface ISellerBillingLogic
    {
        Task<BillingResponseDto> BillingAsync(long              sellerNumber,
                                              IEnumerable<long> articleNumbers = null);

        Task<CancelSettlementResponseDto> CancelSettlementAsync(long articleNumber);
    }
}
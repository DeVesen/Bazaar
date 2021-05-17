using System.Collections.Generic;
using System.Threading.Tasks;
using DeVes.Bazaar.Contracts.Dto;

namespace DeVes.Bazaar.Contracts.Logic
{
    public interface ISaleLogic
    {
        Task<OnSaleResponseDto>      SetOnSaleAsync(long              articleNumber, double price);
        Task<SalesReceiptDto>        SellItemsAsync(IEnumerable<long> articleNumbers);
        Task<RecallsSaleResponseDto> RecallsSaleAsync(long            articleNumber);
    }
}
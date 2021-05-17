using System.Collections.Generic;
using System.Threading.Tasks;
using DeVes.Bazaar.Contracts.Dto;

namespace DeVes.Bazaar.Contracts.Logic
{
    public interface ISaleLogic
    {
        Task<SalesReceiptDto> SellArticlesAsync(IEnumerable<long> articleNumbers);
    }
}
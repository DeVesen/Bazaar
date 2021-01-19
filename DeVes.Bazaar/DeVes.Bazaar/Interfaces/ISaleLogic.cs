using System.Collections.Generic;
using DeVes.Bazaar.Dto;

namespace DeVes.Bazaar.Interfaces
{
    public interface ISaleLogic
    {
        void Sale(IEnumerable<SaleDto> salePositions);
    }
}
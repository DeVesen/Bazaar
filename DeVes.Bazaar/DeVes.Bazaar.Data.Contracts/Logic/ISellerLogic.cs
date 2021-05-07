using System.Collections.Generic;
using System.Threading.Tasks;
using DeVes.Bazaar.Data.Contracts.Models;

namespace DeVes.Bazaar.Data.Contracts.Logic
{
    public interface ISellerLogic
    {
        IEnumerable<SellerModel> GetItems();
        SellerModel GetItem(long number);
        Task<bool> CreateAsync(SellerModel value);
        Task<bool> UpdateAsync(SellerModel value);
        Task<bool> DeleteAsync(long number);
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using DeVes.Bazaar.Contracts.Models;

namespace DeVes.Bazaar.Contracts.Logic
{
    public interface ISellerLogic
    {
        IEnumerable<SellerModel> GetItems(long?  number = null, string firstName = null, string lastName = null,
                                          string zip    = null, string town      = null, string eMail    = null);

        SellerModel GetItem(long number);

        Task<bool> CreateAsync(SellerModel value);
        Task<bool> UpdateAsync(SellerModel value);
        Task<bool> DeleteAsync(long        number);
    }
}
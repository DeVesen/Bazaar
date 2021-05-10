using System.Collections.Generic;
using System.Threading.Tasks;
using DeVes.Bazaar.Contracts.Models;

namespace DeVes.Bazaar.Contracts.Repositories
{
    public interface ISellerRepository
    {
        IEnumerable<SellerModel> GetItems(long? number,
                                          string firstName, string lastName,
                                          string zip, string town, string eMail);
        SellerModel GetItem(long number);

        long GetNextFreeNumber();

        Task<bool> InsertAsync(SellerModel value);
        Task<bool> UpdateAsync(long number, SellerModel value);
        Task<bool> DeleteAsync(long number);
    }
}
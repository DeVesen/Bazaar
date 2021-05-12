using System.Collections.Generic;
using System.Threading.Tasks;
using DeVes.Bazaar.Contracts.Models;

namespace DeVes.Bazaar.Contracts.Repositories
{
    public interface IManufacturerRepository
    {
        long Count { get; }

        IEnumerable<ManufacturerModel> GetItems();
        ManufacturerModel              GetItem(long number);

        long GetNextFreeNumber();

        Task<bool> InsertAsync(ManufacturerModel value);
        Task<bool> UpdateAsync(long              number, ManufacturerModel value);
        Task<bool> DeleteAsync(long              number);
    }
}
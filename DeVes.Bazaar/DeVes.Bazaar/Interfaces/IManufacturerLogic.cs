using System.Collections.Generic;
using System.Threading.Tasks;
using DeVes.Bazaar.Data.Contracts.Models;

namespace DeVes.Bazaar.Interfaces
{
    public interface IManufacturerLogic
    {
        IEnumerable<ManufacturerModel> GetItems();
        ManufacturerModel GetItem(long number);

        Task<bool> CreateAsync(ManufacturerModel value);
        Task<bool> UpdateAsync(ManufacturerModel value);
        Task<bool> DeleteAsync(long number);

        Task BasicInitializationAsync();
    }
}
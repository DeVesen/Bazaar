using System.Collections.Generic;
using System.Threading.Tasks;
using DeVes.Bazaar.Data.Contracts.Models;

namespace DeVes.Bazaar.Interfaces
{
    public interface ICategoryLogic
    {
        IEnumerable<CategoryModel> GetItems();
        CategoryModel GetItem(long number);
        Task<bool> CreateAsync(CategoryModel value);
        Task<bool> UpdateAsync(CategoryModel value);
        Task<bool> DeleteAsync(long number);

        Task BasicInitializationAsync();
    }
}
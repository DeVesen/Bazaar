using System.Collections.Generic;
using System.Threading.Tasks;
using DeVes.Bazaar.Contracts.Models;

namespace DeVes.Bazaar.Contracts.Repositories
{
    public interface ICategoryRepository
    {
        long Count { get; }

        IEnumerable<CategoryModel> GetItems();
        CategoryModel              GetItem(long number);

        long GetNextFreeNumber();

        Task<bool> InsertAsync(CategoryModel value);
        Task<bool> UpdateAsync(long          number, CategoryModel value);
        Task<bool> DeleteAsync(long          number);
    }
}
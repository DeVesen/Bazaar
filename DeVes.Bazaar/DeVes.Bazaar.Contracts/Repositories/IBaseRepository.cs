using System.Collections.Generic;
using System.Threading.Tasks;
using DeVes.Bazaar.Contracts.Models;

namespace DeVes.Bazaar.Contracts.Repositories
{
    public interface IBaseRepository<TModel> : IRepository
        where TModel : BaseModel
    {
        long Count { get; }

        IEnumerable<TModel> GetItems();
        TModel              GetItem(long number);

        long GetNextFreeNumber();

        Task<bool> InsertAsync(TModel value);
        Task<bool> UpdateAsync(long   number, TModel value);
        Task<bool> DeleteAsync(long   number);
    }

    public interface IRepository
    {
    }
}
using System.Collections.Generic;
using DeVes.Bazaar.Data.Contracts.Models;

namespace DeVes.Bazaar.Data.Contracts.Repositories
{
    public interface IBaseRepository<TModel> : IRepository
        where TModel : BaseModel
    {
        long Count { get; }

        IEnumerable<TModel> GetItems();
        TModel GetItem(long number);

        long GetNextFreeNumber();

        void Insert(TModel value);
        void Update(TModel value);

        long Delete(long number);
    }

    public interface IRepository
    {

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeVes.Bazaar.Contracts.Models;
using JsonFlatFileDataStore;

namespace DeVes.Bazaar.Data.JsonDb
{
    public abstract class BaseRepository<T> where T : BaseModel
    {
        protected readonly IDocumentCollection<T> Collection;


        public long Count => Collection.Count;


        protected BaseRepository(RepoOptions repoOptions)
        {
            Collection = repoOptions.DataStoreObj.GetCollection<T>();
        }


        public IEnumerable<T> GetItems()           => Collection.AsQueryable();
        public T              GetItem(long number) => Collection.AsQueryable().FirstOrDefault(p => p.Number == number);

        public async Task<bool> InsertAsync(T value) => await Collection.InsertOneAsync(value);

        public async Task<bool> UpdateAsync(long number, T value) =>
            await Collection.UpdateOneAsync(p => p.Number == number, value);

        public async Task<bool> DeleteAsync(long number) => await Collection.DeleteOneAsync(p => p.Number == number);

        public long GetNextFreeNumber()
        {
            for (long i = 1; i < long.MaxValue; i++)
            {
                if (Collection.AsQueryable().Any(p => p.Number == i) is false)
                    return i;
            }

            throw new Exception("None free Number found!");
        }
    }
}
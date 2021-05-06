using DeVes.Bazaar.Data.Contracts.Models;
using DeVes.Bazaar.Data.Contracts.Repositories;
using JsonFlatFileDataStore;

namespace DeVes.Bazaar.Data.JsonDb
{
    public class CategoryRepository : BaseRepository<CategoryModel>, ICategoryRepository
    {
        public CategoryRepository(RepoOptions repoOptions) : base(repoOptions)
        {
        }
    }
}
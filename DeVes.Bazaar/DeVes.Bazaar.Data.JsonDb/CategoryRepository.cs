using DeVes.Bazaar.Contracts.Models;
using DeVes.Bazaar.Contracts.Repositories;

namespace DeVes.Bazaar.Data.JsonDb
{
    public class CategoryRepository : BaseRepository<CategoryModel>, ICategoryRepository
    {
        public CategoryRepository(RepoOptions repoOptions) : base(repoOptions)
        {
        }
    }
}
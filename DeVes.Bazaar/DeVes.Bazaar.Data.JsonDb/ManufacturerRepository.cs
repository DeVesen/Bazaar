using DeVes.Bazaar.Contracts.Models;
using DeVes.Bazaar.Contracts.Repositories;

namespace DeVes.Bazaar.Data.JsonDb
{
    public class ManufacturerRepository : BaseRepository<ManufacturerModel>, IManufacturerRepository
    {
        public ManufacturerRepository(RepoOptions repoOptions) : base(repoOptions)
        {
        }
    }
}
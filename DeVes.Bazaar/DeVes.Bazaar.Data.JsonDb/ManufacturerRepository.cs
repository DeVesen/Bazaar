using DeVes.Bazaar.Data.Contracts.Models;
using DeVes.Bazaar.Data.Contracts.Repositories;
using JsonFlatFileDataStore;

namespace DeVes.Bazaar.Data.JsonDb
{
    public class ManufacturerRepository : BaseRepository<ManufacturerModel>, IManufacturerRepository
    {
        public ManufacturerRepository(RepoOptions repoOptions) : base(repoOptions)
        {
        }
    }
}
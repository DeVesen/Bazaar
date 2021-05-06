using DeVes.Bazaar.Data.Contracts.Models;
using DeVes.Bazaar.Data.Contracts.Repositories;
using JsonFlatFileDataStore;

namespace DeVes.Bazaar.Data.JsonDb
{
    public class SellerRepository : BaseRepository<SellerModel>, ISellerRepository
    {
        public SellerRepository(RepoOptions repoOptions) : base(repoOptions)
        {
        }
    }
}
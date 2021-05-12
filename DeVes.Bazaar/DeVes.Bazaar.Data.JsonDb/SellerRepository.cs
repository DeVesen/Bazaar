using System;
using System.Collections.Generic;
using System.Linq;
using DeVes.Bazaar.Contracts.Models;
using DeVes.Bazaar.Contracts.Repositories;

namespace DeVes.Bazaar.Data.JsonDb
{
    public class SellerRepository : BaseRepository<SellerModel>, ISellerRepository
    {
        public SellerRepository(RepoOptions repoOptions) : base(repoOptions)
        {
        }

        public IEnumerable<SellerModel> GetItems(long?  number,
                                                 string firstName, string lastName,
                                                 string zip,       string town, string eMail)
        {
            var result = Collection.AsQueryable();

            if (number.HasValue)
                result = result.Where(p => p.Number == number);

            if (string.IsNullOrWhiteSpace(firstName) is false)
                result = result.Where(p => p.FirstName != null && p.FirstName.ToLower()
                                                                   .Contains(firstName.ToLower(),
                                                                             StringComparison.OrdinalIgnoreCase));
            if (string.IsNullOrWhiteSpace(lastName) is false)
                result = result.Where(p => p.LastName != null &&
                                           p.LastName.ToLower()
                                            .Contains(lastName.ToLower(), StringComparison.OrdinalIgnoreCase));

            if (string.IsNullOrWhiteSpace(zip) is false)
                result = result.Where(p => p.Zip != null &&
                                           p.Zip.ToLower().Contains(zip.ToLower(), StringComparison.OrdinalIgnoreCase));
            if (string.IsNullOrWhiteSpace(town) is false)
                result = result.Where(p => p.Town != null &&
                                           p.Town.ToLower()
                                            .Contains(town.ToLower(), StringComparison.OrdinalIgnoreCase));
            if (string.IsNullOrWhiteSpace(eMail) is false)
                result = result.Where(p => p.EMail != null &&
                                           p.EMail.ToLower()
                                            .Contains(eMail.ToLower(), StringComparison.OrdinalIgnoreCase));

            return result;
        }
    }
}
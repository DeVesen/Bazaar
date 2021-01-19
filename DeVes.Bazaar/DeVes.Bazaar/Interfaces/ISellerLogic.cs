using System.Collections.Generic;
using DeVes.Bazaar.Data.Contracts.Models;

namespace DeVes.Bazaar.Interfaces
{
    public interface ISellerLogic
    {
        IEnumerable<SellerModel> GetItems();
        SellerModel GetItem(long number);
        void Create(SellerModel value);
        void Update(SellerModel value);
        void Delete(long number);
    }
}
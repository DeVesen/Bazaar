using System.Collections.Generic;
using DeVes.Bazaar.Data.Contracts.Models;

namespace DeVes.Bazaar.Interfaces
{
    public interface IManufacturerLogic
    {
        IEnumerable<ManufacturerModel> GetItems();
        ManufacturerModel GetItem(long number);
        void Create(ManufacturerModel value);
        void Update(ManufacturerModel value);
        void Delete(long number);

        void BasicInitialization();
    }
}
using System.Collections.Generic;
using DeVes.Bazaar.Data.Contracts.Models;

namespace DeVes.Bazaar.Interfaces
{
    public interface ICategoryLogic
    {
        IEnumerable<CategoryModel> GetItems();
        CategoryModel GetItem(long number);
        void Create(CategoryModel value);
        void Update(CategoryModel value);
        void Delete(long number);

        void BasicInitialization();
    }
}
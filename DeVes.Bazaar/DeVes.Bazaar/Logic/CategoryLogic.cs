using System;
using DeVes.Bazaar.Data.Contracts.Models;
using DeVes.Bazaar.Data.Contracts.Repositories;
using DeVes.Bazaar.Interfaces;

namespace DeVes.Bazaar.Logic
{
    public class CategoryLogic : BaseLogic<CategoryModel>, ICategoryLogic
    {
        private readonly ICategoryRepository _categoryRepository;


        public CategoryLogic(ICategoryRepository categoryRepository)
            : base(categoryRepository)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }


        public void Create(CategoryModel value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            if (string.IsNullOrWhiteSpace(value.Title)) throw new ArgumentException($"'{nameof(value.Title)}' is not defined!");
            
            value.Number = value.Number <= 0
                ? _categoryRepository.GetNextFreeNumber()
                : value.Number;

            if (_categoryRepository.GetItem(value.Number) != null) throw new ArgumentException($"Number '{value.Number}' already in use!");

            _categoryRepository.Insert(value);
        }

        public void Update(CategoryModel value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            if (value.Number <= 0) throw new ArgumentException($"'{nameof(value.Number)}' is not defined!");
            if (string.IsNullOrWhiteSpace(value.Title)) throw new ArgumentException($"'{nameof(value.Title)}' is not defined!");
            
            if (_categoryRepository.GetItem(value.Number) == null) throw new ArgumentException($"{value.Number} not in use!");

            _categoryRepository.Update(value);
        }


        public void BasicInitialization()
        {
            if (_categoryRepository.Count > 0) return;

            Create(new CategoryModel { Title = "Ski" });
            Create(new CategoryModel { Title = "Board" });
            Create(new CategoryModel { Title = "Stöcke" });
            Create(new CategoryModel { Title = "Schuhe" });
            Create(new CategoryModel { Title = "Socken" });
            Create(new CategoryModel { Title = "Hoste" });
            Create(new CategoryModel { Title = "Jacke" });
            Create(new CategoryModel { Title = "Protektor" });
            Create(new CategoryModel { Title = "Termokleidung" });
            Create(new CategoryModel { Title = "Handschuhe" });
            Create(new CategoryModel { Title = "Brille" });
            Create(new CategoryModel { Title = "Helm" });
            Create(new CategoryModel { Title = "Sonstiges" });
        }
    }
}
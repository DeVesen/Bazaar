using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeVes.Bazaar.Data.Contracts.Models;
using DeVes.Bazaar.Data.Contracts.Repositories;
using DeVes.Bazaar.Interfaces;

namespace DeVes.Bazaar.Logic
{
    public class CategoryLogic : ICategoryLogic
    {
        private readonly ICategoryRepository _categoryRepository;


        public CategoryLogic(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        public CategoryModel GetItem(long number) => _categoryRepository.GetItem(number);
        public IEnumerable<CategoryModel> GetItems() => _categoryRepository.GetItems();

        public async Task<bool> CreateAsync(CategoryModel value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            if (string.IsNullOrWhiteSpace(value.Title)) throw new ArgumentException($"'{nameof(value.Title)}' is not defined!");
            
            value.Number = value.Number <= 0
                ? _categoryRepository.GetNextFreeNumber()
                : value.Number;

            if (_categoryRepository.GetItem(value.Number) != null) throw new ArgumentException($"Number '{value.Number}' already in use!");

            return await _categoryRepository.InsertAsync(value);
        }
        public async Task<bool> UpdateAsync(CategoryModel value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            if (value.Number <= 0) throw new ArgumentException($"'{nameof(value.Number)}' is not defined!");
            if (string.IsNullOrWhiteSpace(value.Title)) throw new ArgumentException($"'{nameof(value.Title)}' is not defined!");
            
            if (_categoryRepository.GetItem(value.Number) == null) throw new ArgumentException($"{value.Number} not in use!");

            return await _categoryRepository.UpdateAsync(value.Number, value);
        }
        public async Task<bool> DeleteAsync(long number)
        {
            if (number <= 0) throw new ArgumentException($"'{nameof(number)}' is not defined!");

            return await _categoryRepository.DeleteAsync(number);
        }


        public async Task BasicInitializationAsync()
        {
            if (_categoryRepository.Count > 0) return;

            await CreateAsync(new CategoryModel { Title = "Ski" });
            await CreateAsync(new CategoryModel { Title = "Board" });
            await CreateAsync(new CategoryModel { Title = "Stöcke" });
            await CreateAsync(new CategoryModel { Title = "Schuhe" });
            await CreateAsync(new CategoryModel { Title = "Socken" });
            await CreateAsync(new CategoryModel { Title = "Hoste" });
            await CreateAsync(new CategoryModel { Title = "Jacke" });
            await CreateAsync(new CategoryModel { Title = "Protektor" });
            await CreateAsync(new CategoryModel { Title = "Termokleidung" });
            await CreateAsync(new CategoryModel { Title = "Handschuhe" });
            await CreateAsync(new CategoryModel { Title = "Brille" });
            await CreateAsync(new CategoryModel { Title = "Helm" });
            await CreateAsync(new CategoryModel { Title = "Sonstiges" });
        }
    }
}
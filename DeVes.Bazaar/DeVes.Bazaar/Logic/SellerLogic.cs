using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeVes.Bazaar.Data.Contracts.Logic;
using DeVes.Bazaar.Data.Contracts.Models;
using DeVes.Bazaar.Data.Contracts.Repositories;

namespace DeVes.Bazaar.Logic
{
    public class SellerLogic : ISellerLogic
    {
        private readonly ISellerRepository _sellerRepository;


        public SellerLogic(ISellerRepository sellerRepository)
        {
            _sellerRepository = sellerRepository ?? throw new ArgumentNullException(nameof(sellerRepository));
        }

        public SellerModel GetItem(long number) => _sellerRepository.GetItem(number);
        public IEnumerable<SellerModel> GetItems() => _sellerRepository.GetItems();


        public async Task<bool> CreateAsync(SellerModel value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            if (string.IsNullOrWhiteSpace(value.FirstName)) throw new ArgumentException($"'{nameof(value.FirstName)}' is not defined!");
            if (string.IsNullOrWhiteSpace(value.LastName)) throw new ArgumentException($"'{nameof(value.LastName)}' is not defined!");
            if (string.IsNullOrWhiteSpace(value.Phone)) throw new ArgumentException($"'{nameof(value.Phone)}' is not defined!");

            value.Number = value.Number <= 0
                ? _sellerRepository.GetNextFreeNumber()
                : value.Number;

            if (_sellerRepository.GetItem(value.Number) != null) throw new ArgumentException($"Number '{value.Number}' already in use!");

            return await _sellerRepository.InsertAsync(value);
        }
        public async Task<bool> UpdateAsync(SellerModel value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            if (value.Number <= 0) throw new ArgumentException($"'{nameof(value.Number)}' is not defined!");
            if (string.IsNullOrWhiteSpace(value.FirstName)) throw new ArgumentException($"'{nameof(value.FirstName)}' is not defined!");
            if (string.IsNullOrWhiteSpace(value.LastName)) throw new ArgumentException($"'{nameof(value.LastName)}' is not defined!");
            if (string.IsNullOrWhiteSpace(value.Phone)) throw new ArgumentException($"'{nameof(value.Phone)}' is not defined!");

            if (_sellerRepository.GetItem(value.Number) == null) throw new ArgumentException($"{value.Number} not in use!");

            return await _sellerRepository.UpdateAsync(value.Number, value);
        }
        public async Task<bool> DeleteAsync(long number)
        {
            if (number <= 0) throw new ArgumentException($"'{nameof(number)}' is not defined!");

            return await _sellerRepository.DeleteAsync(number);
        }
    }
}
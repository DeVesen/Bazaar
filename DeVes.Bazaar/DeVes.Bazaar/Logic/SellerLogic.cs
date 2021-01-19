using System;
using DeVes.Bazaar.Data.Contracts.Models;
using DeVes.Bazaar.Data.Contracts.Repositories;
using DeVes.Bazaar.Interfaces;

namespace DeVes.Bazaar.Logic
{
    public class SellerLogic : BaseLogic<SellerModel>, ISellerLogic
    {
        private readonly ISellerRepository _sellerRepository;


        public SellerLogic(ISellerRepository sellerRepository)
            : base(sellerRepository)
        {
            _sellerRepository = sellerRepository ?? throw new ArgumentNullException(nameof(sellerRepository));
        }


        public void Create(SellerModel value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            if (string.IsNullOrWhiteSpace(value.FirstName)) throw new ArgumentException($"'{nameof(value.FirstName)}' is not defined!");
            if (string.IsNullOrWhiteSpace(value.LastName)) throw new ArgumentException($"'{nameof(value.LastName)}' is not defined!");
            if (string.IsNullOrWhiteSpace(value.Phone)) throw new ArgumentException($"'{nameof(value.Phone)}' is not defined!");

            value.Number = value.Number <= 0
                ? _sellerRepository.GetNextFreeNumber()
                : value.Number;

            if (_sellerRepository.GetItem(value.Number) != null) throw new ArgumentException($"Number '{value.Number}' already in use!");

            _sellerRepository.Insert(value);
        }

        public void Update(SellerModel value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            if (value.Number <= 0) throw new ArgumentException($"'{nameof(value.Number)}' is not defined!");
            if (string.IsNullOrWhiteSpace(value.FirstName)) throw new ArgumentException($"'{nameof(value.FirstName)}' is not defined!");
            if (string.IsNullOrWhiteSpace(value.LastName)) throw new ArgumentException($"'{nameof(value.LastName)}' is not defined!");
            if (string.IsNullOrWhiteSpace(value.Phone)) throw new ArgumentException($"'{nameof(value.Phone)}' is not defined!");

            if (_sellerRepository.GetItem(value.Number) == null) throw new ArgumentException($"{value.Number} not in use!");

            _sellerRepository.Update(value);
        }
    }
}
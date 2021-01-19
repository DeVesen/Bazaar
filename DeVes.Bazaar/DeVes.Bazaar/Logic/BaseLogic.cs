using System;
using System.Collections.Generic;
using DeVes.Bazaar.Data.Contracts.Models;
using DeVes.Bazaar.Data.Contracts.Repositories;

namespace DeVes.Bazaar.Logic
{
    public abstract class BaseLogic<TModel>
        where TModel : BaseModel
    {
        private readonly IBaseRepository<TModel> _repository;


        protected BaseLogic(IBaseRepository<TModel> repository)
        {
            _repository = repository;
        }


        public IEnumerable<TModel> GetItems()
        {
            return _repository.GetItems();
        }

        public TModel GetItem(long number)
        {
            return _repository.GetItem(number);
        }

        public void Delete(long number)
        {
            if (number <= 0) throw new ArgumentException($"'{nameof(number)}' is not defined!");
            if (_repository.GetItem(number) == null) throw new ArgumentException($"{number} not found as element!");

            _repository.Delete(number);
        }
    }
}
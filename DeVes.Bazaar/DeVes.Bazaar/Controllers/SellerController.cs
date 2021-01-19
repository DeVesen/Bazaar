using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using DeVes.Bazaar.Data.Contracts.Models;
using System.Linq;
using DeVes.Bazaar.Interfaces;

namespace DeVes.Bazaar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        private readonly ISellerLogic _sellerLogic;


        public SellerController(ISellerLogic sellerLogic)
        {
            _sellerLogic = sellerLogic ?? throw new ArgumentNullException(nameof(sellerLogic));
        }


        // GET: api/<SellerController>
        [HttpGet]
        public IEnumerable<SellerModel> Get()
        {
            return _sellerLogic.GetItems().OrderBy(p => p.LastName)
                                          .ThenBy(p => p.LastName)
                                          .ThenBy(p => p.Number);
        }

        // GET api/<SellerController>/5
        [HttpGet("{number}")]
        public SellerModel Get(int number)
        {
            return _sellerLogic.GetItem(number);
        }

        // POST api/<SellerController>
        [HttpPost]
        public void Post([FromBody] SellerModel value)
        {
            _sellerLogic.Create(value ?? throw new ArgumentNullException(nameof(value)));
        }

        // PUT api/<SellerController>/5
        [HttpPut("{number}")]
        public void Put(int number, [FromBody] SellerModel value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            value.Number = number;

            _sellerLogic.Update(value);
        }

        // DELETE api/<SellerController>/5
        [HttpDelete("{number}")]
        public void Delete(int number)
        {
            if (number <= 0) throw new ArgumentException(nameof(number));

            _sellerLogic.Delete(number);
        }
    }
}

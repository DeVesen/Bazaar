using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeVes.Bazaar.Data.Contracts.Models;
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
        public async Task Post([FromBody] SellerModel value)
        {
            await _sellerLogic.CreateAsync(value ?? throw new ArgumentNullException(nameof(value)));
        }

        // PUT api/<SellerController>/5
        [HttpPut("{number}")]
        public async Task Put(int number, [FromBody] SellerModel value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            value.Number = number;

            await _sellerLogic.UpdateAsync(value);
        }

        // DELETE api/<SellerController>/5
        [HttpDelete("{number}")]
        public async Task Delete(int number)
        {
            if (number <= 0) throw new ArgumentException(nameof(number));

            await _sellerLogic.DeleteAsync(number);
        }
    }
}

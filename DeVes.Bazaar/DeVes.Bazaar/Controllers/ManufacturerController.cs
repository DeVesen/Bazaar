using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeVes.Bazaar.Contracts.Logic;
using DeVes.Bazaar.Contracts.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeVes.Bazaar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturerController : ControllerBase
    {
        private readonly IManufacturerLogic _manufacturerLogic;


        public ManufacturerController(IManufacturerLogic manufacturerLogic)
        {
            _manufacturerLogic = manufacturerLogic ?? throw new ArgumentNullException(nameof(manufacturerLogic));
        }


        // GET: api/<ArticleController>
        [HttpGet]
        public IEnumerable<ManufacturerModel> Get()
        {
            return _manufacturerLogic.GetItems().OrderBy(p => p.Title);
        }

        // GET api/<ArticleController>/5
        [HttpGet("{number}")]
        public ManufacturerModel Get(int number)
        {
            return _manufacturerLogic.GetItem(number);
        }

        // POST api/<ArticleController>
        [HttpPost]
        public async Task Post([FromBody] ManufacturerModel value)
        {
            await _manufacturerLogic.CreateAsync(value ?? throw new ArgumentNullException(nameof(value)));
        }

        // PUT api/<ArticleController>/5
        [HttpPut("{number}")]
        public async Task Put(int number, [FromBody] ManufacturerModel value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            value.Number = number;

            await _manufacturerLogic.UpdateAsync(value);
        }

        // DELETE api/<ArticleController>/5
        [HttpDelete("{number}")]
        public async Task Delete(int number)
        {
            if (number <= 0) throw new ArgumentException(nameof(number));

            await _manufacturerLogic.DeleteAsync(number);
        }
    }
}
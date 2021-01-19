using System;
using System.Collections.Generic;
using System.Linq;
using DeVes.Bazaar.Data.Contracts.Models;
using DeVes.Bazaar.Interfaces;
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
        public void Post([FromBody] ManufacturerModel value)
        {
            _manufacturerLogic.Create(value ?? throw new ArgumentNullException(nameof(value)));
        }

        // PUT api/<ArticleController>/5
        [HttpPut("{number}")]
        public void Put(int number, [FromBody] ManufacturerModel value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            value.Number = number;

            _manufacturerLogic.Update(value);
        }

        // DELETE api/<ArticleController>/5
        [HttpDelete("{number}")]
        public void Delete(int number)
        {
            if (number <= 0) throw new ArgumentException(nameof(number));

            _manufacturerLogic.Delete(number);
        }
    }
}
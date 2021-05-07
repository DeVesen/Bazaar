using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeVes.Bazaar.Data.Contracts.Logic;
using DeVes.Bazaar.Data.Contracts.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeVes.Bazaar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryLogic _categoryLogic;


        public CategoryController(ICategoryLogic categoryLogic)
        {
            _categoryLogic = categoryLogic ?? throw new ArgumentNullException(nameof(categoryLogic));
        }


        // GET: api/<ArticleController>
        [HttpGet]
        public IEnumerable<CategoryModel> Get()
        {
            return _categoryLogic.GetItems().OrderBy(p => p.Title);
        }

        // GET api/<ArticleController>/5
        [HttpGet("{number}")]
        public CategoryModel Get(int number)
        {
            return _categoryLogic.GetItem(number);
        }

        // POST api/<ArticleController>
        [HttpPost]
        public async Task Post([FromBody] CategoryModel value)
        {
            await _categoryLogic.CreateAsync(value ?? throw new ArgumentNullException(nameof(value)));
        }

        // PUT api/<ArticleController>/5
        [HttpPut("{number}")]
        public async Task Put(int number, [FromBody] CategoryModel value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            value.Number = number;

            await _categoryLogic.UpdateAsync(value);
        }

        // DELETE api/<ArticleController>/5
        [HttpDelete("{number}")]
        public async Task Delete(int number)
        {
            if (number <= 0) throw new ArgumentException(nameof(number));

            await _categoryLogic.DeleteAsync(number);
        }
    }
}
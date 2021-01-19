using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using DeVes.Bazaar.Data.Contracts.Models;
using DeVes.Bazaar.Interfaces;
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
        public void Post([FromBody] CategoryModel value)
        {
            _categoryLogic.Create(value ?? throw new ArgumentNullException(nameof(value)));
        }

        // PUT api/<ArticleController>/5
        [HttpPut("{number}")]
        public void Put(int number, [FromBody] CategoryModel value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            value.Number = number;

            _categoryLogic.Update(value);
        }

        // DELETE api/<ArticleController>/5
        [HttpDelete("{number}")]
        public void Delete(int number)
        {
            if (number <= 0) throw new ArgumentException(nameof(number));

            _categoryLogic.Delete(number);
        }
    }


    public class ResponseModel<TData>
    {
        public TData Data { get; }
        public long ElapsedMilliseconds { get; }


        public ResponseModel(TData data, Stopwatch stopwatch)
        {
            Data = data;
            ElapsedMilliseconds = stopwatch.ElapsedMilliseconds;
        }
    }
}
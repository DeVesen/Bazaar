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
    public class ArticleController : ControllerBase
    {
        private readonly IArticleLogic _articleLogic;


        public ArticleController(IArticleLogic articleLogic)
        {
            _articleLogic = articleLogic ?? throw new ArgumentNullException(nameof(articleLogic));
        }


        // GET: api/<ArticleController>
        [HttpGet]
        public IEnumerable<ArticleModel> Get()
        {
            return _articleLogic.GetItems().OrderBy(p => p.Number);
        }

        // GET api/<ArticleController>/5
        [HttpGet("{number}")]
        public ArticleModel Get(int number)
        {
            return _articleLogic.GetItem(number);
        }

        // POST api/<ArticleController>
        [HttpPost]
        public void Post([FromBody] ArticleModel value)
        {
            _articleLogic.Create(value ?? throw new ArgumentNullException(nameof(value)));
        }

        // PUT api/<ArticleController>/5
        [HttpPut("{number}")]
        public void Put(int number, [FromBody] ArticleModel value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            value.Number = number;

            _articleLogic.Update(value);
        }

        // DELETE api/<ArticleController>/5
        [HttpDelete("{number}")]
        public void Delete(int number)
        {
            if (number <= 0) throw new ArgumentException(nameof(number));

            _articleLogic.Delete(number);
        }
    }
}
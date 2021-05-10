﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeVes.Bazaar.Contracts.Logic;
using DeVes.Bazaar.Contracts.Models;
using DeVes.Bazaar.Extensions;
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
            var reqNumber = Request.Query.Get<long?>("number");
            var reqSellerNumber = Request.Query.Get<long?>("sellerNumber");
            var reqTitle = Request.Query.Get<string>("title");
            var reqCategory = Request.Query.Get<string>("category");
            var reqManufacturer = Request.Query.Get<string>("manufacturer");
            var reqIsSold = Request.Query.Get<bool?>("isSold");
            var reqIsReturned = Request.Query.Get<bool?>("isReturned");

            return _articleLogic
                .GetItems(reqNumber, reqSellerNumber, reqTitle, reqCategory, reqManufacturer, reqIsSold, reqIsReturned)
                .OrderBy(p => p.Number);
        }

        // GET api/<ArticleController>/5
        [HttpGet("{number}")]
        public ArticleModel Get(int number)
        {
            return _articleLogic.GetItem(number);
        }

        // POST api/<ArticleController>
        [HttpPost]
        public async Task Post([FromBody] ArticleModel value)
        {
            await _articleLogic.CreateAsync(value ?? throw new ArgumentNullException(nameof(value)));
        }

        // PUT api/<ArticleController>/5
        [HttpPut("{number}")]
        public async Task Put(int number, [FromBody] ArticleModel value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            value.Number = number;

            await _articleLogic.UpdateAsync(value);
        }

        // DELETE api/<ArticleController>/5
        [HttpDelete("{number}")]
        public async Task Delete(int number)
        {
            if (number <= 0) throw new ArgumentException(nameof(number));

            await _articleLogic.DeleteAsync(number);
        }
    }
}
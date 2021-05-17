using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeVes.Bazaar.Contracts.Logic;
using DeVes.Bazaar.Contracts.Models;
using DeVes.Bazaar.Extensions;

namespace DeVes.Bazaar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        private readonly ISellerLogic  _sellerLogic;
        private readonly IArticleLogic _articleLogic;


        public SellerController(ISellerLogic sellerLogic, IArticleLogic articleLogic)
        {
            _sellerLogic  = sellerLogic ?? throw new ArgumentNullException(nameof(sellerLogic));
            _articleLogic = articleLogic ?? throw new ArgumentNullException(nameof(articleLogic));
        }


        // GET: api/<SellerController>
        [HttpGet]
        public IEnumerable<SellerModel> Get()
        {
            var reqNumber    = Request.Query.Get<long?>("number");
            var reqFirstName = Request.Query.Get<string>("firstName");
            var reqTitle     = Request.Query.Get<string>("lastName");
            var reqZip       = Request.Query.Get<string>("zip");
            var reqTown      = Request.Query.Get<string>("town");
            var reqEMail     = Request.Query.Get<string>("eMail");

            return _sellerLogic.GetItems(reqNumber, reqFirstName, reqTitle, reqZip, reqTown, reqEMail)
                               .OrderBy(p => p.LastName)
                               .ThenBy(p => p.LastName)
                               .ThenBy(p => p.Number);
        }

        // GET api/<SellerController>/5
        [HttpGet("{number}")]
        public SellerModel Get(int number)
        {
            return _sellerLogic.GetItem(number);
        }

        // GET api/<SellerController>/5/Articles
        [HttpGet("{sellerNumber}/Articles")]
        public IEnumerable<ArticleModel> GetArticles(int sellerNumber)
        {
            return _articleLogic.GetItems(null, sellerNumber);
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
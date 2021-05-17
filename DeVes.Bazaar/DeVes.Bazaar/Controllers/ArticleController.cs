using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeVes.Bazaar.Contracts.Dto;
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
        private readonly IArticleLogic       _articleLogic;
        private readonly ISaleLogic          _saleLogic;
        private readonly ISellerBillingLogic _sellerBillingLogic;

        public ArticleController(IArticleLogic       articleLogic,
                                 ISaleLogic          saleLogic,
                                 ISellerBillingLogic sellerBillingLogic)
        {
            _articleLogic       = articleLogic ?? throw new ArgumentNullException(nameof(articleLogic));
            _saleLogic          = saleLogic ?? throw new ArgumentNullException(nameof(sellerBillingLogic));
            _sellerBillingLogic = sellerBillingLogic ?? throw new ArgumentNullException(nameof(sellerBillingLogic));
        }

        // GET: api/<ArticleController>
        [HttpGet]
        public IEnumerable<ArticleModel> Get()
        {
            var reqNumber       = Request.Query.Get<long?>("number");
            var reqSellerNumber = Request.Query.Get<long?>("sellerNumber");
            var reqTitle        = Request.Query.Get<string>("title");
            var reqCategory     = Request.Query.Get<string>("category");
            var reqManufacturer = Request.Query.Get<string>("manufacturer");
            var reqIsSold       = Request.Query.Get<bool?>("isSold");
            var reqIsReturned   = Request.Query.Get<bool?>("isReturned");

            return _articleLogic
                   .GetItems(reqNumber, reqSellerNumber, reqTitle, reqCategory, reqManufacturer, reqIsSold,
                             reqIsReturned)
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
        public async Task Post([FromBody] ArticleInsertDto value)
        {
            await _articleLogic.CreateAsync(value ?? throw new ArgumentNullException(nameof(value)));
        }

        // PUT api/<ArticleController>/5
        [HttpPut("{number}")]
        public async Task Put(int number, [FromBody] ArticleUpdateDto value)
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



        // Put api/<ArticleController>/5/marked
        [HttpPut("{number}/marked")]
        public async Task<MarkedResponseDto> SetOnMarkedAsync(int number)
        {
            return await _articleLogic.SetArticleOnMarkedAsync(number, null);
        }

        // Put api/<ArticleController>/5/marked/12.99
        [HttpPut("{number}/marked/{price:double}")]
        public async Task<MarkedResponseDto> SetOnMarkedAsync(int number, double price)
        {
            return await _articleLogic.SetArticleOnMarkedAsync(number, price);
        }

        // DELETE api/<ArticleController>/5/marked
        [HttpDelete("{number}/marked")]
        public async Task<MarkedResponseDto> RemoveFromMarkedAsync(int number)
        {
            return await _articleLogic.RemoveArticleFromMarkedAsync(number);
        }

        

        // PUT api/<ArticleController>/sale
        [HttpPut("/sale")]
        public async Task<SalesReceiptDto> SellArticlesAsync([FromBody] IEnumerable<long> articleNumbers)
        {
            return await _saleLogic.SellArticlesAsync(articleNumbers);
        }

        // DELETE api/<ArticleController>/5/billing
        [HttpDelete("{number}/billing")]
        public async Task CancelSettlementAsync(int number)
        {
            if (number <= 0) throw new ArgumentException(nameof(number));
            await _sellerBillingLogic.CancelSettlementAsync(number);
        }
    }
}
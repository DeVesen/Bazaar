using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DeVes.Bazaar.Contracts.Dto;
using DeVes.Bazaar.Contracts.Logic;
using Microsoft.AspNetCore.Mvc;

namespace DeVes.Bazaar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly ISaleLogic _saleLogic;


        public SaleController(ISaleLogic saleLogic)
        {
            _saleLogic = saleLogic ?? throw new ArgumentNullException(nameof(saleLogic));
        }


        // Put api/<SellerController>
        [HttpPut]
        public async Task<OnSaleResponseDto> SetOnSaleAsync([FromBody] OnSaleRequestDto request)
        {
            return await _saleLogic.SetOnSaleAsync(request.ArticleNumber, request.Price);
        }

        // POST api/<SellerController>
        [HttpPost]
        public async Task<SalesReceiptDto> SellItemsAsync([FromBody] IEnumerable<long> articleNumbers)
        {
            return await _saleLogic.SellItemsAsync(articleNumbers);
        }

        // DELETE api/<SellerController>/5
        [HttpDelete("{number}")]
        public async Task<RecallsSaleResponseDto> RecallsSaleAsync(int number)
        {
            return await _saleLogic.RecallsSaleAsync(number);
        }
    }
}
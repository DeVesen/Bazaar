using System;
using System.Collections.Generic;
using DeVes.Bazaar.Dto;
using DeVes.Bazaar.Interfaces;
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
            _saleLogic = saleLogic;
        }


        // POST api/<SaleController>
        [HttpPost]
        public void Sale([FromBody] IEnumerable<SaleDto> salePositions)
        {
            _saleLogic.Sale(salePositions ?? throw new ArgumentNullException(nameof(salePositions)));
        }
    }
}
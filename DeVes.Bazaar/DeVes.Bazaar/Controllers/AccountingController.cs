using Microsoft.AspNetCore.Mvc;
using DeVes.Bazaar.Dto;
using DeVes.Bazaar.Interfaces;

namespace DeVes.Bazaar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountingController : ControllerBase
    {
        private readonly IAccountingLogic _accountingLogic;


        public AccountingController(IAccountingLogic accountingLogic)
        {
            _accountingLogic = accountingLogic;
        }


        // GET: api/<AccountingController>
        [HttpGet("{number}")]
        public AccountingListDto GetAccountingList(int number)
        {
            return _accountingLogic.GetAccountingList(number);
        }

        // POST api/<AccountingController>
        [HttpPost("{number}")]
        public void ReturnRemainingArticles(int number)
        {
            _accountingLogic.ReturnRemainingArticles(number);
        }
    }
}
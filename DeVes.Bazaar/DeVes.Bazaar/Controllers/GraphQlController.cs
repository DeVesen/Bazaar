using System;
using System.Threading.Tasks;
using DeVes.Bazaar.GraphQl;
using DeVes.Bazaar.GraphQl.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace DeVes.Bazaar.Controllers
{
    [Route("api/graphql")]
    [ApiController]
    public class GraphQlController : ControllerBase
    {
        private readonly GraphQlLogic _graphQlLogic;


        public GraphQlController(GraphQlLogic graphQlLogic)
        {
            _graphQlLogic = graphQlLogic ?? throw new ArgumentNullException(nameof(graphQlLogic));
        }


        // GET: api/<ArticleController>
        [Route("api/[controller]")]
        [HttpPost]
        public async Task<string> ExecuteAsync([FromBody] GraphQlRequest request)
        {
            // "{ seller(sellerNumber: 3) { number lastName articles { number sellerNumber title} } }"
            return await _graphQlLogic.ExecuteAsync(request);
        }
    }
}
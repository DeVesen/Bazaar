using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using DeVes.Bazaar.Data.Contracts.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace DeVes.Bazaar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnvironmentVariableController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration      _configuration;


        public EnvironmentVariableController(IWebHostEnvironment environment,
                                             IConfiguration      configuration)
        {
            _environment   = environment;
            _configuration = configuration;
        }


        // GET: api/<EnvironmentVariableController>
        [HttpGet]
        public IEnumerable<EnvironmentVariable> Get()
        {
            var envVariables = _configuration.AsEnumerable().Select(p => new EnvironmentVariable
            {
                Key = p.Key,
                Value = p.Value
            }).ToList();


            envVariables.Add(new EnvironmentVariable
            {
                Key = "WebRootPath",
                Value = _environment.WebRootPath
            });
            envVariables.Add(new EnvironmentVariable
            {
                Key = "ContentRootPath",
                Value = _environment.ContentRootPath
            });

            return envVariables;
        }

        // GET api/<EnvironmentVariableController>/5
        [HttpGet("{key}")]
        public EnvironmentVariable Get(string key)
        {
            return Get().FirstOrDefault(p => p.Key.Equals(key, StringComparison.OrdinalIgnoreCase));
        }
    }
}

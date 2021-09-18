using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KeyGeneratorService.Models;
using KeyGeneratorService.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace KeyGeneratorService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatsController : ControllerBase
    {
        private readonly ILogger<StatsController> _logger;

        public StatsController(ILogger<StatsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = new StatsDTO()
            {
                Generated = StatsSingletonService.Generated,
                Collisions = StatsSingletonService.Collisions
            };
            return Ok(result);
        }
    }
}
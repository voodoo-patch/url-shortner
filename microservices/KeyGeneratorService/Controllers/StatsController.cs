﻿using System;
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
        private readonly object statsService;

        public StatsController(ILogger<StatsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(StatsSingletonService.StatsInstance);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UrlShortener.Services;

namespace UrlShortener.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShortenerController : ControllerBase
    {
        private readonly ILogger<ShortenerController> _logger;
        private readonly IShortenerService shortenerService;

        public ShortenerController(ILogger<ShortenerController> logger, IShortenerService shortenerService)
        {
            _logger = logger;
            this.shortenerService = shortenerService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string url)
        {
            string shortned = await this.shortenerService.Shorten(url);
            
            return Ok(shortned);
        }
    }
}

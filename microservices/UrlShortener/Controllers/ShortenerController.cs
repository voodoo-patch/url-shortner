using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UrlShortener.Models;
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

        [HttpPost]
        public async Task<IActionResult> Post([FromQuery] string url)
        {
            string shortned = await this.shortenerService.Shorten(url);

            return Ok(shortned);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string key)
        {
            TinyUrlDTO tinyUrl = await this.shortenerService.GetUrl(key);
            if (tinyUrl == null)
            {
                return NotFound();
            }

            return Redirect(tinyUrl.OriginalUrl);
        }
    }
}
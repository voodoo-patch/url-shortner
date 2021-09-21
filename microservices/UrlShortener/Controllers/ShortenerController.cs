using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UrlShortener.Models;
using UrlShortener.Services;

namespace UrlShortener.Controllers
{
    [ApiController]
    [Route("")]
    public class ShortenerController : ControllerBase
    {
        private readonly ILogger<ShortenerController> _logger;
        private readonly IShortenerService shortenerService;
        private readonly Regex httpSuffix = new Regex(@"^https?:\/\/");

        public ShortenerController(ILogger<ShortenerController> logger, IShortenerService shortenerService)
        {
            _logger = logger;
            this.shortenerService = shortenerService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromQuery] string url)
        {
            if (!string.IsNullOrEmpty(url) && !httpSuffix.IsMatch(url))
            {
                url = "http://" + url;
            }

            bool isUri = Uri.IsWellFormedUriString(url, UriKind.Absolute);
            if (!isUri)
            {
                return BadRequest("Provided URL is not in a correct format");
            }

            string shortned = await this.shortenerService.Shorten(url);

            return Ok(shortned);
        }

        [HttpGet("{key}")]
        public async Task<IActionResult> Get(string key)
        {
            TinyUrlDTO tinyUrl = await this.shortenerService.GetUrl(key);
            if (tinyUrl == null)
            {
                return NotFound();
            }

            string redirectUrl = tinyUrl.OriginalUrl;
            if (!httpSuffix.IsMatch(redirectUrl))
            {
                redirectUrl = "http://" + redirectUrl;
            }

            return Redirect(redirectUrl);
        }
    }
}
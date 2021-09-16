using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UrlShortner.Services;

namespace UrlShortner.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShortnerController : ControllerBase
    {
        private readonly ILogger<ShortnerController> _logger;
        private readonly IShortnerService shortnerService;

        public ShortnerController(ILogger<ShortnerController> logger, IShortnerService shortnerService)
        {
            _logger = logger;
            this.shortnerService = shortnerService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string url)
        {
            string shortned = await this.shortnerService.Shorten(url);
            
            return Ok(shortned);
        }
    }
}

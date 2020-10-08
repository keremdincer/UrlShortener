using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Contracts;

namespace UrlShortener.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlsController : ControllerBase
    {
        private readonly IShortUrlRepository _shortUrlRepository;

        public UrlsController(IShortUrlRepository shortUrlRepository)
        {
            _shortUrlRepository = shortUrlRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var shortUrls = await _shortUrlRepository.FindAll();
            return Ok(shortUrls);
        }
    }
}

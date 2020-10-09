using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Contracts;

namespace UrlShortener.Controllers
{
    [Route("")]
    public class RouterController : Controller
    {
        private readonly IShortUrlRepository _shortUrlRepository;

        public RouterController(IShortUrlRepository shortUrlRepository)
        {
            _shortUrlRepository = shortUrlRepository;
        }

        /// <summary>
        /// Kısa url'lerin yönlendirmesini yapar.
        /// </summary>
        /// <param name="token">Kısa url kodu</param>
        /// <remarks>
        /// Not: Bu method documentation sayfası üzerinde yönlendirme yapmaz.
        /// </remarks>
        /// <returns></returns>
        /// <response code="308">Kısa url geçerli ise yönlendirme yapar.</response>
        /// <response code="404">Karşılıksız bir kısa url ise</response>
        [Route("{token}")]
        [ProducesResponseType(StatusCodes.Status308PermanentRedirect)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<IActionResult> Index(string token)
        {
            var shortUrl = await _shortUrlRepository.FindByToken(token);
            if (shortUrl == null)
                return NotFound();

            // Check if short url's expiration
            if (shortUrl.ExpiresAt.HasValue && shortUrl.ExpiresAt.Value < DateTime.Now)
                return NotFound();

            return Redirect(shortUrl.OriginalUrl);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UAParser;
using UrlShortener.Contracts;
using UrlShortener.Data;

namespace UrlShortener.Controllers
{
    [Route("")]
    public class RouterController : Controller
    {
        private readonly IShortUrlRepository _shortUrlRepository;
        private readonly IUsageLogRepository _usageLogRepository;

        public RouterController(
            IShortUrlRepository shortUrlRepository,
            IUsageLogRepository usageLogRepository
        )
        {
            _shortUrlRepository = shortUrlRepository;
            _usageLogRepository = usageLogRepository;
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

            var clientInfo = Parser.GetDefault().Parse(Request.Headers["User-Agent"]);

            // TODO: Use NLog for this
            // Log usage info
            var usageLog = new UsageLog
            {
                ShortUrlId = shortUrl.Id,
                ClientIP = HttpContext.Connection.RemoteIpAddress.ToString(),
                ClientBrowser = clientInfo.UA.Family,
                ClientDevice = clientInfo.Device.Family,
                ClientOS = clientInfo.OS.Family
            };

            await _usageLogRepository.Create(usageLog);

            return Redirect(shortUrl.OriginalUrl);
        }
    }
}

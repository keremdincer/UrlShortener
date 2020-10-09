using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Contracts;
using UrlShortener.Data;
using UrlShortener.Dtos;

namespace UrlShortener.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrlsController : ControllerBase
    {
        private readonly IShortUrlRepository _shortUrlRepository;
        private readonly IUsageLogRepository _usageLogRepository;
        private readonly IMapper _mapper;
        private readonly INanoIdGenerator _nanoIdGenerator;

        public UrlsController(
            IShortUrlRepository shortUrlRepository,
            IUsageLogRepository usageLogRepository,
            IMapper mapper,
            INanoIdGenerator nanoIdGenerator
        )
        {
            _shortUrlRepository = shortUrlRepository;
            _usageLogRepository = usageLogRepository;
            _mapper = mapper;
            _nanoIdGenerator = nanoIdGenerator;
        }

        /// <summary>
        /// Tüm kısa url'leri sayfalara bölerek getirir.
        /// </summary>
        /// <param name="pageSize">Sayfa büyüklüğü</param>
        /// <param name="pageNo">Sayfa numarası</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int pageSize = 50, int pageNo = 1)
        {
            var shortUrls = await _shortUrlRepository.FindAll(pageSize, pageNo - 1);
            var count = await _shortUrlRepository.Count();
            var shortUrlDtos = _mapper.Map<IList<ShortUrlDto>>(shortUrls);

            var response = new
            {
                pageCount = (int)Math.Ceiling((decimal)count / pageSize),
                pageNo,
                data = shortUrlDtos
            };

            return Ok(response);
        }

        /// <summary>
        /// Id'si belirtilen kısa url'i getirir.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Id'ye karşılık gelen kısa url'i getirir.</response>
        /// <response code="204">Eğer Id'nin karşılığı yoksa NoContent status'ü döner.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get(int id)
        {
            var shortUrl = await _shortUrlRepository.FindById(id);
            var response = _mapper.Map<ShortUrlDto>(shortUrl);
            return Ok(response);
        }

        /// <summary>
        /// Yeni bir kısa url oluşturur.
        /// </summary>
        /// <param name="shortUrlDto">Kısa url bilgileri</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] ShortUrlCreateDto shortUrlDto)
        {
            var shortUrl = _mapper.Map<ShortUrl>(shortUrlDto);
            shortUrl.Url = _nanoIdGenerator.Generate();
            shortUrl.ApiKeyId = 1;
            shortUrl.isPermanent = false;

            var success = await _shortUrlRepository.Create(shortUrl);
            if (!success)
                return BadRequest();

            var response = _mapper.Map<ShortUrlDto>(shortUrl);
            return Created("Create", new { response });
        }

        /// <summary>
        /// Id'si belirtilen kısa url'i günceller.
        /// </summary>
        /// <param name="id">Kısa url id'si</param>
        /// <param name="shortUrlDto">Güncellenecek bilgiler</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] ShortUrlUpdateDto shortUrlDto
        )
        {
            var shortUrl = await _shortUrlRepository.FindById(id);
            if (shortUrl == null)
                return BadRequest();

            // Patch only supplied fields
            _mapper.Map(shortUrlDto, shortUrl);

            var success = await _shortUrlRepository.Update(shortUrl);
            if (!success)
                return BadRequest();

            var response = _mapper.Map<ShortUrlDto>(shortUrl);
            return Ok(response);
        }

        /// <summary>
        /// Id'si belirtilen kısa url'i siler.
        /// </summary>
        /// <param name="id">Kısa url id'si</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(int id)
        {
            var shortUrl = await _shortUrlRepository.FindById(id);
            if (shortUrl == null)
                return BadRequest();

            var success = await _shortUrlRepository.Delete(shortUrl);
            if (!success)
                return BadRequest();

            return NoContent();
        }

        /// <summary>
        /// Id'si belirtilen kısa url'in kullanım bilgilerini getirir.
        /// </summary>
        /// <param name="id">Kısa url id'si</param>
        /// <param name="pageSize">Sayfa büyüklüğü</param>
        /// <param name="pageNo">Sayfa numarası</param>
        /// <returns></returns>
        [HttpGet("{id}/logs")]
        public async Task<IActionResult> GetUsageLogs(int id, int pageSize = 50, int pageNo = 1)
        {
            var useLogs = await _usageLogRepository.FindAllByUrlId(id, pageSize, pageNo - 1);
            var count = await _usageLogRepository.CountByUrlId(id);
            var useLogDtos = _mapper.Map<IList<UsageLogDto>>(useLogs);

            var response = new
            {
                pageCount = (int)Math.Ceiling((decimal)count / pageSize),
                pageNo,
                data = useLogDtos
            };

            return Ok(response);
        }
    }
}

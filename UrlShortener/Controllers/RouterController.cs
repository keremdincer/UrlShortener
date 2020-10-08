﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        [Route("{token}")]
        [HttpGet]
        public IActionResult Index(string token)
        {
            return Redirect("https://www.youtube.com");
        }
    }
}

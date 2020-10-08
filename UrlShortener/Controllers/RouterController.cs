using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace UrlShortener.Controllers
{
    [Route("")]
    public class RouterController : Controller
    {
        [Route("{token}")]
        [HttpGet]
        public IActionResult Index(string token)
        {
            return Redirect("https://www.youtube.com");
        }
    }
}

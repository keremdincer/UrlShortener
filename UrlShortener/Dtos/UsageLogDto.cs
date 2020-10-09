using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrlShortener.Dtos
{
    public class UsageLogDto
    {
        public string ClientIP { get; set; }
        public string ClientBrowser { get; set; }
        public string ClientDevice { get; set; }
        public string ClientOS { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrlShortener.Dtos
{
    public class ShortUrlDto
    {
        public int Id { get; set; }
        public string OriginalUrl { get; set; }
        public string Url { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ExpiresAt { get; set; }
    }

    public class ShortUrlCreateDto
    {
        public string OriginalUrl { get; set; }
        public DateTime? ExpiresAt { get; set; }
    }

    public class ShortUrlUpdateDto
    {
        public string OriginalUrl { get; set; }
        public DateTime? ExpiresAt { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UrlShortener.Data
{
    [Table("ShortUrlUsage")]
    public class UsageLog
    {
        public int Id { get; set; }
        public int ShortUrlId { get; set; }
        public string ClientIP { get; set; }
        public string ClientBrowser { get; set; }
        public string ClientDevice { get; set; }
        public string ClientOS { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual ShortUrl ShortUrl { get; set; }

        public UsageLog ()
        {
            CreatedAt = DateTime.Now;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UrlShortener.Data
{
    [Table("ShortUrl")]
    public class ShortUrl
    {
        public int Id { get; set; }
        public string OriginalUrl { get; set; }
        public string Url { get; set; }
        public bool isPermanent { get; set; }
        public int? CreatorId { get; set; }
        public int ApiKeyId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ExpiresAt { get; set; }

        public virtual ICollection<UsageLog> UsageLogs { get; set; }

        public ShortUrl()
        {
            CreatedAt = DateTime.Now;
        }
    }
}

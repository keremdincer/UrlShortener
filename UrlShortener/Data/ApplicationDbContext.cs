using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrlShortener.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<ShortUrl> ShortUrls { get; set; }
        public DbSet<UsageLog> UsageLogs { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlShortener.Contracts;
using UrlShortener.Data;

namespace UrlShortener.Services
{
    public class UsageLogRepository : IUsageLogRepository
    {
        private readonly ApplicationDbContext _db;

        public UsageLogRepository(ApplicationDbContext db) => _db = db;

        public async Task<int> CountByUrlId(int id)
        {
            return await _db.UsageLogs
                .Where(ul => ul.ShortUrlId == id)
                .CountAsync();
        }

        public async Task<IList<UsageLog>> FindAllByUrlId(int id, int pageSize, int pageNo)
        {
            return await _db.UsageLogs
                .Where(ul => ul.ShortUrlId == id)
                .Skip(pageSize * pageNo)
                .Take(pageSize)
                .AsQueryable()
                .ToListAsync();
        }
    }
}

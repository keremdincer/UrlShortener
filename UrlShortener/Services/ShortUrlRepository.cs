using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlShortener.Contracts;
using UrlShortener.Data;

namespace UrlShortener.Services
{
    public class ShortUrlRepository : IShortUrlRepository
    {
        private readonly ApplicationDbContext _db;

        public ShortUrlRepository(ApplicationDbContext db) => _db = db;

        public async Task<bool> Create(ShortUrl entity)
        {
            await _db.ShortUrls.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(ShortUrl entity)
        {
            _db.ShortUrls.Remove(entity);
            return await Save();
        }

        public async Task<IList<ShortUrl>> FindAll()
        {
            var shortUrls = await _db.ShortUrls.ToListAsync();
            return shortUrls;
        }

        public async Task<ShortUrl> FindById(int id)
        {
            var shortUrl = await _db.ShortUrls.FindAsync(id);
            return shortUrl;
        }

        public async Task<bool> IsExists(int id)
        {
            return await _db.ShortUrls.AnyAsync(shortUrl => shortUrl.Id == id);
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(ShortUrl entity)
        {
            _db.ShortUrls.Update(entity);
            return await Save();
        }
    }
}

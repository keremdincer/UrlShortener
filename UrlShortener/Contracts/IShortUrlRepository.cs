using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlShortener.Data;

namespace UrlShortener.Contracts
{
    public interface IShortUrlRepository
    {
        Task<IList<ShortUrl>> FindAll(int pageSize, int pageNo);
        Task<int> Count();

        Task<ShortUrl> FindById(int id);
        Task<bool> IsExists(int id);
        Task<bool> Create(ShortUrl entity);
        Task<bool> Update(ShortUrl entity);
        Task<bool> Delete(ShortUrl entity);
        Task<bool> Save();
        Task<ShortUrl> FindByToken(string token);
    }
}

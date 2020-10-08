using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlShortener.Data;

namespace UrlShortener.Contracts
{
    public interface IShortUrlRepository : IRepositoryBase<ShortUrl>
    {
    }
}

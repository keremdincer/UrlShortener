using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlShortener.Data;

namespace UrlShortener.Contracts
{
    public interface IUsageLogRepository
    {
        Task<IList<UsageLog>> FindAllByUrlId(int id, int pageSize, int pageNo);

        Task<int> CountByUrlId(int id);
    }
}

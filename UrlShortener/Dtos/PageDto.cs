using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrlShortener.Dtos
{
    public class PageDto<T> where T : class
    {
        public int PageNo { get; set; }
        public int PageCount { get; set; }
        public IList<T> Data { get; set; }
    }
}

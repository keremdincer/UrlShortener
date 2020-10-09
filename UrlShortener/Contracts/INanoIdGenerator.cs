using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrlShortener.Contracts
{
    public interface INanoIdGenerator
    {
        string Generate();
    }
}

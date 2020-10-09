using Microsoft.Extensions.Configuration;
using Nanoid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlShortener.Contracts;

namespace UrlShortener.Services
{
    public class NanoIdGenerator : INanoIdGenerator
    {
        private readonly IConfiguration _config;

        public NanoIdGenerator(IConfiguration config) => _config = config;

        public string Generate()
        {
            var nanoid = Nanoid.Nanoid.Generate(
                _config["NanoId:Domain"],
                int.Parse(_config["NanoId:Length"])
            );

            return nanoid;
        }
    }
}

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlShortener.Data;

namespace UrlShortener.Dtos
{
    public class Maps : Profile
    {
        public Maps()
        {
            CreateMap<ShortUrl, ShortUrlDto>().ReverseMap();
            CreateMap<ShortUrl, ShortUrlCreateDto>().ReverseMap();
            CreateMap<ShortUrlUpdateDto, ShortUrl>()
                .ForMember(dest => dest.ExpiresAt, opt => opt.MapFrom(src => src.ExpiresAt))
                .ForMember(dest => dest.OriginalUrl, opt => opt.MapFrom(src => src.OriginalUrl))
                .ForAllMembers(opt => opt.Condition((src, dest, value) => value != null));

            CreateMap<UsageLog, UsageLogDto>();
        }
    }
}

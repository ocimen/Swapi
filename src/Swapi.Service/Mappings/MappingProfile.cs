using System.Linq;
using AutoMapper;
using Swapi.Service.Models;

namespace Swapi.Service.Mappings
{
    public class MappingProfile : Profile
    {
        private const string LocalUrl = "http://localhost:5000/api/";
        public MappingProfile(SwapApi swapApi)
        {
            CreateMap<People, People>()
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url.Replace(swapApi.BaseUrl, LocalUrl)))
                .ForMember(dest => dest.films, opt => opt.MapFrom(src => src.films.Select(f => f.Replace(swapApi.BaseUrl, LocalUrl))))
                .ForMember(dest => dest.species, opt => opt.MapFrom(src => src.species.Select(f => f.Replace(swapApi.BaseUrl, LocalUrl))))
                .ForMember(dest => dest.starships, opt => opt.MapFrom(src => src.starships.Select(f => f.Replace(swapApi.BaseUrl, LocalUrl))))
                .ForMember(dest => dest.vehicles, opt => opt.MapFrom(src => src.vehicles.Select(f => f.Replace(swapApi.BaseUrl, LocalUrl))))
                .ForMember(dest => dest.homeworld, opt => opt.MapFrom(src => src.homeworld.Replace(swapApi.BaseUrl, LocalUrl)));

            CreateMap<Planet, Planet>()
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url.Replace(swapApi.BaseUrl, LocalUrl)))
                .ForMember(dest => dest.Films, opt => opt.MapFrom(src => src.Films.Select(f => f.Replace(swapApi.BaseUrl, LocalUrl))))
                .ForMember(dest => dest.Residents, opt => opt.MapFrom(src => src.Residents.Select(f => f.Replace(swapApi.BaseUrl, LocalUrl))));

            CreateMap<SearchResult<People>, SearchResult<People>>()
                .ForMember(d => d.previous, opt => opt.MapFrom(src => src.previous.Replace(swapApi.BaseUrl, LocalUrl)))
                .ForMember(d => d.next, opt => opt.MapFrom(src => src.next.Replace(swapApi.BaseUrl, LocalUrl)));
            
            CreateMap<SearchResult<Planet>, SearchResult<Planet>>()
                .ForMember(d => d.previous, opt => opt.MapFrom(src => src.previous.Replace(swapApi.BaseUrl, LocalUrl)))
                .ForMember(d => d.next, opt => opt.MapFrom(src => src.next.Replace(swapApi.BaseUrl, LocalUrl)));
        }
    }
}
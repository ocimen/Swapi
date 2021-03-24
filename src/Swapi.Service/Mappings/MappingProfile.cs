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
                .ForMember(dest => dest.Films, opt => opt.MapFrom(src => src.Films.Select(f => f.Replace(swapApi.BaseUrl, LocalUrl))))
                .ForMember(dest => dest.Species, opt => opt.MapFrom(src => src.Species.Select(f => f.Replace(swapApi.BaseUrl, LocalUrl))))
                .ForMember(dest => dest.Starships, opt => opt.MapFrom(src => src.Starships.Select(f => f.Replace(swapApi.BaseUrl, LocalUrl))))
                .ForMember(dest => dest.Vehicles, opt => opt.MapFrom(src => src.Vehicles.Select(f => f.Replace(swapApi.BaseUrl, LocalUrl))))
                .ForMember(dest => dest.Homeworld, opt => opt.MapFrom(src => src.Homeworld.Replace(swapApi.BaseUrl, LocalUrl)));

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
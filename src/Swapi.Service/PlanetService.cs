using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Swapi.Service.Interfaces;
using Swapi.Service.Models;

namespace Swapi.Service
{
    public class PlanetService : IPlanetService
    {
        private readonly ILogger _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IOptions<SwapApi> _options;
        private readonly IMapper _mapper;

        public PlanetService(ILogger<PlanetService> logger, IHttpClientFactory httpClientFactory, IOptions<SwapApi> options, IMapper mapper)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _options = options;
            _mapper = mapper;
        }

        public async Task<Planet> GetById(int id)
        {
            var httpClient = _httpClientFactory.CreateClient(_options.Value.SwapiClient);
            var url = $"planets/{id}/";
            var response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation($"Planet Get By Id {id} was successful");
                var planet = await GetPlanet(response.Content);
                return planet;
            }
            
            _logger.LogWarning($"Request to {url} has failed");
            return null;
        }

        public async Task<SearchResult<Planet>> Search(string name, int page)
        {
            var httpClient = _httpClientFactory.CreateClient(_options.Value.SwapiClient);
            var url = $"planets/?search={name}&page={page}";
            var response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation($"Planet Search Get By {name} was successful");
                var planetList = await GetPlanetSearchResult(response.Content);
                return planetList;
            }
            
            _logger.LogWarning($"Request to {url} has failed");
            return new SearchResult<Planet>();
        }

        private async Task<Planet> GetPlanet(HttpContent content)
        {
            var planetContent = await content.ReadAsStringAsync();
            var deserializedPlanet = JsonConvert.DeserializeObject<Planet>(planetContent);
            if (deserializedPlanet != null)
            {
                var mappedPlanet = _mapper.Map<Planet>(deserializedPlanet);
                return mappedPlanet;
            }

            return null;
        }

        private async Task<SearchResult<Planet>> GetPlanetSearchResult(HttpContent content)
        {
            var searchResultContent = await content.ReadAsStringAsync();
            var deserializedPlanetList = JsonConvert.DeserializeObject<SearchResult<Planet>>(searchResultContent);
            if (deserializedPlanetList != null)
            {
                var mappedPlanetList = _mapper.Map<SearchResult<Planet>>(deserializedPlanetList);
                mappedPlanetList.results = _mapper.Map<List<Planet>>(mappedPlanetList.results);
                return mappedPlanetList;
            }

            return null;
        }
    }
}

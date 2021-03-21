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
        private readonly ILogger _Logger;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IOptions<SwapApi> _options;
        private readonly IMapper _mapper;

        public PlanetService(ILogger<PlanetService> logger, IHttpClientFactory httpClientFactory, IOptions<SwapApi> options, IMapper mapper)
        {
            _Logger = logger;
            this.httpClientFactory = httpClientFactory;
            _options = options;
            _mapper = mapper;
        }

        public async Task<Planet> GetById(int id)
        {
            var httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_options.Value.BaseUrl);
            var url = $"planets/{id}/";
            var response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                _Logger.LogInformation($"Planet Get By Id {id} was successfull");
                var content = await response.Content.ReadAsStringAsync();
                var planet = JsonConvert.DeserializeObject<Planet>(content);
                planet = _mapper.Map<Planet>(planet);
                return planet;
            }
            
            _Logger.LogWarning($"Request to {url} has failed");
            return null;
        }

        public async Task<SearchResult<Planet>> Search(string name, int page = 1)
        {
            var httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_options.Value.BaseUrl);
            var url = $"planets/?search={name}&page={page}";
            var response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var planetList = JsonConvert.DeserializeObject<SearchResult<Planet>>(content);
                planetList = _mapper.Map<SearchResult<Planet>>(planetList);
                planetList.results = _mapper.Map<List<Planet>>(planetList.results);
                return planetList;
            }
            
            _Logger.LogWarning($"Request to {url} has failed");
            return null;
        }
    }
}

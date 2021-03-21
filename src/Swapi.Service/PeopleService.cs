using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Swapi.Service.Interfaces;
using Swapi.Service.Models;

namespace Swapi.Service
{
    public class PeopleService : IPeopleService
    {
        private readonly ILogger _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IOptions<SwapApi> _options;
        private readonly IMapper _mapper;

        public PeopleService(ILogger<PeopleService> logger, IHttpClientFactory httpClientFactory, IOptions<SwapApi> options, IMapper mapper)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _options = options;
            _mapper = mapper;
        }

        public async Task<People> GetById(int id)
        {
            var httpClient = _httpClientFactory.CreateClient(_options.Value.ClientName);
            var url = $"people/{id}/";
            var response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation($"People Get By Id {id} was successfull");
                var people = await GetPeople(response.Content);
                return people;
            }
            
            _logger.LogWarning($"Request to {url} has failed");
            return null;
        }

        public async Task<SearchResult<People>> Search(string name, int page)
        {
            var httpClient = _httpClientFactory.CreateClient(_options.Value.ClientName);
            var url = $"people/?search={name}&page={page}";
            var response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation($"People Search Get By {name} was successfull");
                var person = await GetPeopleSearchResult(response.Content);
                return person;
            }

            _logger.LogWarning($"Request to {url} has failed");
            return null;
        }

        private async Task<People> GetPeople(HttpContent content)
        {
            var str = await content.ReadAsStringAsync();
            var people = JsonConvert.DeserializeObject<People>(str);
            if (people != null)
            {
                people = _mapper.Map<People>(people);
                return people;
            }

            return null;
        }

        private async Task<SearchResult<People>> GetPeopleSearchResult(HttpContent content)
        {
            var searchResult = await content.ReadAsStringAsync();
            var people = JsonConvert.DeserializeObject<SearchResult<People>>(searchResult);
            if (people != null)
            {
                people = _mapper.Map<SearchResult<People>>(people);
                people.results = _mapper.Map<List<People>>(people.results);
                return people;
            }

            return null;
        }
    }
}
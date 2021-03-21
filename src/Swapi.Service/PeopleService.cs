using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Swapi.Service.Interfaces;
using Swapi.Service.Models;

namespace Swapi.Service
{
    public class PeopleService : IPeopleService
    {
        private readonly ILogger _Logger;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IOptions<SwapApi> _options;

        public PeopleService(ILogger<PeopleService> logger, IHttpClientFactory httpClientFactory, IOptions<SwapApi> options)
        {
            _Logger = logger;
            this.httpClientFactory = httpClientFactory;
            _options = options;
        }

        public async Task<People> GetById(int id)
        {
            var httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_options.Value.BaseUrl);
            var url = $"people/{id}/";
            var response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                _Logger.LogInformation($"People Get By Id {id} was successfull");
                var content = await response.Content.ReadAsStringAsync();
                var person = JsonConvert.DeserializeObject<People>(content);
                ModifyUrls(person);
                return person;
            }
            
            _Logger.LogWarning($"Request to {url} has failed");
            return null;
        }

        public async Task<SearchResult<People>> GetByName(string name)
        {
            var httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(_options.Value.BaseUrl);
            var url = $"people/?search={name}";
            var response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var person = JsonConvert.DeserializeObject<SearchResult<People>>(content);
                return person;
            }

            _Logger.LogWarning($"Request to {url} has failed");
            return null;
        }

        private void ModifyUrls(People people)
        {
            //TODO: get base url from config or dynamic
            people.homeworld = people.homeworld.Replace("swapi.dev", "localhost:5000");
            people.url = people.url.Replace("swapi.dev", "localhost:5000");
            people.films = people.films.Select(s => s.Replace("swapi.dev", "localhost:5000")).ToList();
            people.vehicles = people.vehicles.Select(s => s.Replace("swapi.dev", "localhost:5000")).ToList();
            people.species = people.species.Select(s => s.Replace("swapi.dev", "localhost:5000")).ToList();
        }
    }
}
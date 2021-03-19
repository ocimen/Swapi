using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Swapi.Service.Interfaces;
using Swapi.Service.Models;

namespace Swapi.Service
{
    public class PlanetService : IPlanetService
    {
        private readonly ILogger _Logger;
        private readonly IHttpClientFactory httpClientFactory;

        public PlanetService(ILogger<PlanetService> logger, IHttpClientFactory httpClientFactory)
        {
            _Logger = logger;
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<object> GetById(int id)
        {
            var httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri("https://swapi.dev");
            var response = await httpClient.GetAsync($"/api/planets/{id}/");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                if (content != null)
                {
                    var planet = JsonConvert.DeserializeObject<Planet>(content);
                    return planet;
                }
            }

            return null;
        }

        public async Task<object> GetByName(string name)
        {
            var httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri("https://swapi.dev");
            var response = await httpClient.GetAsync($"/api/planets/?search={name}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                if (content != null)
                {
                    var planet = JsonConvert.DeserializeObject<Planet>(content);
                    return planet;
                }
            }

            return null;
        }
    }
}

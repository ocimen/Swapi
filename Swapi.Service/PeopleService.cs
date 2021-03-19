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
    public class PeopleService : IPeopleService
    {
        private readonly ILogger _Logger;
        private readonly IHttpClientFactory httpClientFactory;

        public PeopleService(ILogger<PeopleService> logger, IHttpClientFactory httpClientFactory)
        {
            _Logger = logger;
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<People> GetById(int id)
        {
            var httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri("https://swapi.dev");
            var response = await httpClient.GetAsync($"/api/people/{id}/");
            if (response.IsSuccessStatusCode)
            {
                _Logger.LogInformation($"People Get By Id {id} was successfull");
                var content = await response.Content.ReadAsStringAsync();
                if (content != null)
                {
                    var person = JsonConvert.DeserializeObject<People>(content);
                    return person;
                }
            }

            return null;
        }

        public async Task<object> GetByName(string name)
        {
            var httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri("https://swapi.dev");
            var response = await httpClient.GetAsync($"/api/people/?search={name}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                if (content != null)
                {
                    var person = JsonConvert.DeserializeObject<People>(content);
                    return person;
                }
            }

            return null;
        }
    }
}
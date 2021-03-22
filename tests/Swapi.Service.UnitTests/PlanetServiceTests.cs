using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using Swapi.Service.Interfaces;
using Swapi.Service.Models;
using Xunit;
using ILogger = Castle.Core.Logging.ILogger;

namespace Swapi.Service.UnitTests
{
    public class PlanetServiceTests : TestBase
    {
        private readonly IPlanetService _planetService;

        public PlanetServiceTests()
        {
            var loggerMock = new Mock<ILogger<PlanetService>>();
            _planetService = new PlanetService(loggerMock.Object, httpClientMock.Object, optionsMock.Object, mapper);
        }

        [Fact]
        public async Task Planet_GetByID_Success()
        {
            SetupClient(HttpStatusCode.OK, new StringContent(ContentSamples.PlanetContent));
            var planet = await _planetService.GetById(2);

            Assert.NotNull(planet);
            Assert.Equal("Alderaan", planet.Name);
        }

        [Fact]
        public async Task Planet_GetById_NotFound()
        {
            SetupClient(HttpStatusCode.NotFound, new StringContent(string.Empty));
            var planet = await _planetService.GetById(0);
            Assert.Null(planet);
        }

        [Fact]
        public async Task Planet_Search_Success()
        {
            SetupClient(HttpStatusCode.OK, new StringContent(ContentSamples.PlanetSearchResultContent));
            var searchResult = await _planetService.Search("alee", 1);
            Assert.NotNull(searchResult);
            Assert.NotNull(searchResult.results);
            Assert.True(searchResult.results.Count > 0);
        }

        [Fact]
        public async Task Planet_Search_NotFound()
        {
            SetupClient(HttpStatusCode.NotFound, new StringContent(string.Empty));
            var searchResult = await _planetService.Search("Not Existing Planet", 1);
            Assert.NotNull(searchResult);
            Assert.Null(searchResult.results);
        }
    }
}

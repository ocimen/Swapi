using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Swapi.Service.Interfaces;
using Xunit;

namespace Swapi.Service.UnitTests
{
    public class PeopleServiceTests : TestBase
    {
        private readonly IPeopleService _peopleService;
        
        public PeopleServiceTests()
        {
            var loggerMock = new Mock<ILogger<PeopleService>>();
            _peopleService = new PeopleService(loggerMock.Object, httpClientMock.Object, optionsMock.Object, mapper);
        }

        [Fact]
        public async Task People_GetByID_Success()
        {
            SetupClient(HttpStatusCode.OK, new StringContent(ContentSamples.PeopleContent));
            var people = await _peopleService.GetById(2);

            Assert.NotNull(people);
            Assert.Equal("C-3PO", people.name);
        }

        [Fact]
        public async Task People_GetById_NotFound()
        {
            SetupClient(HttpStatusCode.NotFound, new StringContent(string.Empty));
            var people = await _peopleService.GetById(0);
            Assert.Null(people);
        }

        [Fact]
        public async Task People_Search_Success()
        {
            SetupClient(HttpStatusCode.OK, new StringContent(ContentSamples.PeopleSearchResultContent));
            var searchResult = await _peopleService.Search("Lu", 1);
            Assert.NotNull(searchResult);
            Assert.NotNull(searchResult.results);
            Assert.True(searchResult.results.Count > 0);
        }

        [Fact]
        public async Task People_Search_NotFound()
        {
            SetupClient(HttpStatusCode.NotFound, new StringContent(string.Empty));
            var searchResult = await _peopleService.Search("Not Existing People", 1);
            Assert.NotNull(searchResult);
            Assert.Null(searchResult.results);
        }
    }
}

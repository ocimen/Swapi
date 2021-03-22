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
using Swapi.Service.Mappings;
using Swapi.Service.Models;

namespace Swapi.Service.UnitTests
{
    public class TestBase
    {
        public readonly Mock<IHttpClientFactory> httpClientMock;
        public readonly Mock<IOptions<SwapApi>> optionsMock;
        public readonly IMapper mapper;

        public TestBase()
        {
            optionsMock = new Mock<IOptions<SwapApi>>();
            optionsMock.Setup(s => s.Value).Returns(new SwapApi
            {
                BaseUrl = "http://localhost/",
                SwapiClient = "Swapi"
            });

            httpClientMock = new Mock<IHttpClientFactory>();
            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile(optionsMock.Object.Value));
            });

            mapper = mapConfig.CreateMapper();
        }

        public  void SetupClient(HttpStatusCode statusCode, StringContent content)
        {
            

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = statusCode,
                    Content = content,
                });

            var client = new HttpClient(mockHttpMessageHandler.Object);
            client.BaseAddress = new Uri(optionsMock.Object.Value.BaseUrl);
            httpClientMock.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
        }
    }
}

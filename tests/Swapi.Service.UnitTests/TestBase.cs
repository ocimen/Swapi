using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
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
        public readonly Mock<IOptions<SwapApi>> optionsSwapiMock;
        public readonly IMapper mapper;

        public TestBase()
        {
            optionsSwapiMock = new Mock<IOptions<SwapApi>>();
            optionsSwapiMock.Setup(s => s.Value).Returns(new SwapApi
            {
                BaseUrl = "http://localhost/",
                SwapiClient = "Swapi"
            });

            httpClientMock = new Mock<IHttpClientFactory>();
            var mapConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile(optionsSwapiMock.Object.Value));
            });

            mapper = mapConfig.CreateMapper();
        }

        public  void SetupClient(HttpStatusCode statusCode, StringContent content)
        {
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = statusCode,
                    Content = content,
                });

            var client = new HttpClient(mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri(optionsSwapiMock.Object.Value.BaseUrl)
            };
            httpClientMock.Setup(s => s.CreateClient(It.IsAny<string>())).Returns(client);
        }
    }
}

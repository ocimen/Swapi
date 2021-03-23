using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Swapi.Service.Interfaces;
using Swapi.Service.Models;
using Xunit;

namespace Swapi.Service.UnitTests
{
    public class AuthServiceTests : TestBase
    {
        private readonly IAuthService _authService;
        private readonly Mock<IOptions<Audience>> optionsAudienceMock;

        public AuthServiceTests()
        {
            var loggerMock = new Mock<ILogger<AuthService>>();

            optionsAudienceMock = new Mock<IOptions<Audience>>();
            optionsAudienceMock.Setup(s => s.Value).Returns(new Audience
            {
                Aud = "aud",
                Iss = "Iss",
                Secret = "Y2F0Y2hlciUyMHdvbmclMjBsb3ZlJTIwLm5ldA=="
            });

            _authService = new AuthService(loggerMock.Object, optionsAudienceMock.Object, optionsSwapiMock.Object);
        }

        [Fact]
        public void User_Should_Get_Token_With_Correct_Credentials()
        {
            optionsSwapiMock.Object.Value.UserName = "username";
            optionsSwapiMock.Object.Value.Password = "password";
            
            var token = _authService.Login(optionsSwapiMock.Object.Value.UserName, optionsSwapiMock.Object.Value.Password);
            Assert.NotNull(token);
            Assert.Equal(60, token.ExpiresIn);
        }

        [Fact]
        public void User_ShouldNot_Get_Token_With_Wrong_Credentials()
        {
            var token = _authService.Login(string.Empty, string.Empty);
            Assert.Null(token);
        }
    }
}

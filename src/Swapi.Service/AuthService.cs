using System;
using System.Globalization;
using System.Security.Claims;
using System.Text;
using Swapi.Service.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swapi.Service.Models;

namespace Swapi.Service
{
    public class AuthService : IAuthService
    {
        private readonly ILogger _logger;
        private readonly IOptions<Audience> _audienceOptions;
        private readonly IOptions<SwapApi> _swapApiOptions;

        public AuthService(ILogger<AuthService> logger, IOptions<Audience> audienceOptions, IOptions<SwapApi> swapApiOptions)
        {
            _logger = logger;
            _audienceOptions = audienceOptions;
            _swapApiOptions = swapApiOptions;
        }

        public SwapiToken Login(string username, string password)
        {
            if (username == _swapApiOptions.Value.UserName && password == _swapApiOptions.Value.Password)
            {
                _logger.LogInformation($"{username} has logged in successfully");
                return GenerateToken(username);
            }

            _logger.LogInformation($"{username} could not authorized");
            return null;
        }

        private SwapiToken GenerateToken(string username)
        {
            var now = DateTime.UtcNow;

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(CultureInfo.InvariantCulture), ClaimValueTypes.Integer64)
            };

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_audienceOptions.Value.Secret));

            var jwt = new JwtSecurityToken(
                issuer:_audienceOptions.Value.Iss,
                audience: _audienceOptions.Value.Aud,
                claims: claims,
                notBefore: now,
                expires: now.Add(TimeSpan.FromMinutes(60)),
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            var responseJson = new SwapiToken
            {
                AccessToken = encodedJwt,
                ExpiresIn = (int)TimeSpan.FromMinutes(60).TotalMinutes,
                Type = "Bearer"
            };

            _logger.LogInformation($"Token created for {username}.");
            return responseJson;
        }
    }
}

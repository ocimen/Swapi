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
        private readonly IOptions<Audience> _options;

        public AuthService(ILogger<AuthService> logger, IOptions<Audience> options)
        {
            _logger = logger;
            _options = options;
        }

        public object Login(string username, string password)
        {
            if (username == "user" && password == "pwd")
            {
                _logger.LogInformation($"{username} has logged in successfully");
                var now = DateTime.UtcNow;

                var claims = new Claim[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(CultureInfo.InvariantCulture), ClaimValueTypes.Integer64)
                };

                var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_options.Value.Secret));

                var jwt = new JwtSecurityToken(
                    issuer: "localhost",
                    audience: "Test User",
                    claims: claims,
                    notBefore: now,
                    expires: now.Add(TimeSpan.FromMinutes(500)),
                    signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
                );
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
                var responseJson = new
                {
                    access_token = encodedJwt,
                    expires_in = (int)TimeSpan.FromMinutes(5000).TotalSeconds
                };
                
                _logger.LogInformation($"Token created for {username}.");
                return responseJson;
            }

            _logger.LogInformation($"{username} could not authorized");
            return null;
        }
    }
}

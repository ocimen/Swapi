using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Swapi.Models;
using Swapi.Service.Interfaces;

namespace Swapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Action to login with username and password.
        /// </summary>
        /// <param name="model">Model to login with username and password</param>
        /// <returns>Returns the boolean result of login</returns>
        /// <response code="200">Returned if the login was successful</response>
        /// <response code="400">Returned if the model couldn't be parsed or the login was not made</response>
        /// <response code="422">Returned when the validation failed</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<bool> Login(LoginModel model)
        {
            var result = _authService.Login(model.UserName, model.Password);
            if (result)
            {
                return Ok(true);
            }

            return BadRequest("Wrong username or password");
        }
    }
}

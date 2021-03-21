using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Swapi.Service.Interfaces;

namespace Swapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanetController : ControllerBase
    {
        private readonly IPlanetService planetService;

        public PlanetController(IPlanetService planetService)
        {
            this.planetService = planetService;
        }

        /// <summary>
        /// Action to get specific planet.
        /// </summary>
        /// <param name="id">Param to get specific planet by id</param>
        /// <returns>Returns the specific planet</returns>
        /// <response code="200">Returned if the there is a planet with that id</response>
        /// <response code="400">Returned if there is no planet with that id</response>
        [Authorize]
        [HttpGet("{id:int}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var planet = await planetService.GetById(id);
            if (planet != null)
            {
                return Ok(planet);
            }

            return NotFound();
        }

        /// <summary>
        /// Action to get specific planet.
        /// </summary>
        /// <param name="name">Param to get specific planet by name</param>
        /// <returns>Returns the specific planet</returns>
        /// <response code="200">Returned if the there is a planet with that id</response>
        /// <response code="400">Returned if there is no planet with that id</response>
        [Authorize]
        [HttpGet("{name}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(string name)
        {
            var planet = await planetService.GetByName(name);
            if (planet != null)
            {
                return Ok(planet);
            }

            return NotFound();
        }
    }
}

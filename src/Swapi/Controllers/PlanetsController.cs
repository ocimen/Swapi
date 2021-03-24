using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Swapi.Service.Interfaces;

namespace Swapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanetsController : ControllerBase
    {
        private readonly IPlanetService _planetService;

        public PlanetsController(IPlanetService planetService)
        {
            this._planetService = planetService;
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
            var planet = await _planetService.GetById(id);
            if (planet != null)
            {
                return Ok(planet);
            }

            return NotFound();
        }

        /// <summary>
        /// Action to search specific planet by name.
        /// </summary>
        /// <param name="search">Param to search planet by name</param>
        /// <param name="page">Param to get specific page of the results.</param>
        /// <returns>Returns the planets list which matched with search term</returns>
        /// <response code="200">Returned if there is any matched planet with that name</response>
        /// <response code="404">Returned if there is no planet with that name</response>
        [Authorize]
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Search(string search, int page = 1)
        {
            var planet = await _planetService.Search(search, page);
            if (planet != null)
            {
                return Ok(planet);
            }

            return NotFound();
        }
    }
}

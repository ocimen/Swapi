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
    public class PeopleController : ControllerBase
    {
        private readonly IPeopleService peopleService;

        public PeopleController(IPeopleService peopleService)
        {
            this.peopleService = peopleService;
        }

        /// <summary>
        /// Action to get specific people by id
        /// </summary>
        /// <param name="id">Param to get specific people by id</param>
        /// <returns>Returns the specific people</returns>
        /// <response code="200">Returned if there is any people with that id</response>
        /// <response code="404">Returned if there is no people with that id</response>
        [Authorize]
        [HttpGet("{id:int}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var people = await peopleService.GetById(id);
            if (people != null)
            {
                return Ok(people);
            }

            return NotFound();
        }

        /// <summary>
        /// Action to get specific people by name
        /// </summary>
        /// <param name="name">Param to get specific people by name</param>
        /// <returns>Returns the specific people</returns>
        /// <response code="200">Returned if there is any people with that id</response>
        /// <response code="404">Returned if there is no people with that id</response>
        [Authorize]
        [HttpGet("{name}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(string name)
        {
            var people = await peopleService.GetByName(name);
            if (people != null)
            {
                return Ok(people);
            }

            return NotFound();
        }
    }
}

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
        private readonly IPeopleService _peopleService;

        public PeopleController(IPeopleService peopleService)
        {
            this._peopleService = peopleService;
        }

        /// <summary>
        /// Action to get specific people by id
        /// </summary>
        /// <param name="id">Param to get specific people by id</param>
        /// <returns>Returns the specific people</returns>
        /// <response code="200">Returned if there is any people with that id</response>
        /// <response code="404">Returned if there is no people with that id</response>
        //TODO: open authorize
        //[Authorize]
        [HttpGet("{id:int}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(int id)
        {
            var people = await _peopleService.GetById(id);
            if (people != null)
            {
                return Ok(people);
            }

            return NotFound();
        }

        /// <summary>
        /// Action to search specific people by name
        /// </summary>
        /// <param name="name">Param to search people by name</param>
        /// <param name="page">Param to get specific page of the results.</param>
        /// <returns>Returns the people list which matched with search term</returns>
        /// <response code="200">Returned if there is any matched people with that name</response>
        /// <response code="404">Returned if there is no people with that name</response>
        //TODO: open authorize
        //[Authorize]
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Search(string name, int page = 1)
        {
            var people = await _peopleService.Search(name, page);
            if (people != null)
            {
                return Ok(people);
            }

            return NotFound();
        }
    }
}

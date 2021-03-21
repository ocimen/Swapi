
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Swapi.Service.Models;

namespace Swapi.Service.Interfaces
{
    public interface IPlanetService
    {
        Task<Planet> GetById(int id);

        Task<SearchResult<Planet>> GetByName(string name);
    }
}

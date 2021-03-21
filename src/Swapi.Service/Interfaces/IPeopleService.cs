using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Swapi.Service.Models;

namespace Swapi.Service.Interfaces
{
    public interface IPeopleService
    {
        Task<People> GetById(int id);

        Task<SearchResult<People>> GetByName(string name);
    }
}

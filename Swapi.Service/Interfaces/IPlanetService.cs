
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Swapi.Service.Interfaces
{
    public interface IPlanetService
    {
        Task<object> GetById(int id);

        Task<object> GetByName(string name);
    }
}

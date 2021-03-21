using System;
using System.Collections.Generic;
using System.Text;

namespace Swapi.Service.Interfaces
{
    public interface IAuthService
    {
        object Login(string username, string password);
    }
}

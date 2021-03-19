using System;
using System.Collections.Generic;
using System.Text;

namespace Swapi.Service.Interfaces
{
    public interface IAuthService
    {
        bool Login(string username, string password);
    }
}

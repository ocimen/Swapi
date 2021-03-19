using System;
using System.Collections.Generic;
using System.Text;
using Swapi.Service.Interfaces;

namespace Swapi.Service
{
    public class AuthService : IAuthService
    {
        public bool Login(string username, string password)
        {
            return username == "user" && password == "pwd";
        }
    }
}

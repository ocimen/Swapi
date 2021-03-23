using Swapi.Service.Models;

namespace Swapi.Service.Interfaces
{
    public interface IAuthService
    {
        SwapiToken Login(string username, string password);
    }
}

namespace Swapi.Service.Models
{
    public class SwapiToken
    {
        public string AccessToken { get; set; }
        public string Type { get; set; }
        public int ExpiresIn { get; set; }
    }
}

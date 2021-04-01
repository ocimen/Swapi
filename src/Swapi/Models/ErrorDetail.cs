using System.Net;

namespace Swapi.Models
{
    public class ErrorDetail
    {
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.InternalServerError;

        public string ErrorMessage { get; set; } = "An unexpected error occured";
    }
}
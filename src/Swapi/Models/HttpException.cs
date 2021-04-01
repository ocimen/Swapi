﻿using System;
using System.Net;

namespace Swapi.Models
{
    public class HttpException :Exception
    {
        public HttpStatusCode StatusCode { get; set; }

        public HttpException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}

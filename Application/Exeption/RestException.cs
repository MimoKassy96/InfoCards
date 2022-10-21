using System;
using System.Net;

namespace Application.Exeption
{
    /// <summary>
    /// Rest Exception
    /// </summary>
    public class RestException : Exception
    {
        /// <summary>
        /// Gets http status code
        /// </summary>
        public HttpStatusCode Code { get; }
        /// <summary>
        /// Gets or sets the error
        /// </summary>
        public object Errors { get; set; }

        public RestException(HttpStatusCode code, object errors = null)
        {
            Code = code;
            Errors = errors;
        }
    }
}

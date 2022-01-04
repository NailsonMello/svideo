using System;
using System.Net;
using System.Runtime.Serialization;

namespace SVideo.Application.Exceptions
{
    [Serializable]
    public class HttpException : Exception, ISerializable
    {
        public string Type { get; set; }
        public string Url { get; set; }
        public HttpStatusCode Code { get; set; }

        public HttpException(string type, string url, HttpStatusCode code, string message)
            : this(message)
        {
            Type = type;
            Url = url;
            Code = code;
        }

        private HttpException()
        {
        }

        private HttpException(string message) : base(message)
        {
        }

        private HttpException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

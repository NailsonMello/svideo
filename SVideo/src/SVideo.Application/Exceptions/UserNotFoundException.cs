using System;
using System.Runtime.Serialization;

namespace SVideo.Application.Exceptions
{
    [Serializable]
    public class UserNotFoundException : Exception
    {
        public int HttpCode { get; set; }

        protected UserNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public UserNotFoundException(string message, int httpCode = 401) : base(message)
        {
            HttpCode = httpCode;
        }
    }
}

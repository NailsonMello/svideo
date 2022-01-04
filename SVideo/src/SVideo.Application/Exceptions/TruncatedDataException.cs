using System;
using System.Runtime.Serialization;

namespace SVideo.Application.Exceptions
{
    [Serializable]
    public class TruncatedDataException : Exception
    {
        public string Key { get; }
        public int HttpCode { get; set; }

        protected TruncatedDataException(SerializationInfo info, StreamingContext context) : base(info, context) { }

        public TruncatedDataException(string key, Exception exception, int httpCode = 400) : base("Ocorreu um ou mais erros ao persistir os dados.", exception)
        {
            Key = key;
            HttpCode = httpCode;
        }
    }
}

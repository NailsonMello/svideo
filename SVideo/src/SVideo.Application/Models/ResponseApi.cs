using System.Collections.Generic;

namespace SVideo.Application.Models
{
    public class ResponseApi
    {
        public ResponseApi(bool success, string message, object data = null)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }

    public class ApplicationResponse
    {
        public ApplicationResponse(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
    }

    public class ApplicationResponseStruct<T> : ApplicationResponse
        where T : struct
    {
        public ApplicationResponseStruct(bool success, string message, T data)
            : base(success, message)
        {
            Data = data;
        }

        public T Data { get; private set; }
    }

    public class ApplicationResponseItem<T> : ApplicationResponse
        where T : class
    {
        public ApplicationResponseItem(bool success, string message, T data)
            : base(success, message)
        {
            Data = data;
        }

        public T Data { get; private set; }
    }

    public class ApplicationResponseList<T> : ApplicationResponse
        where T : class
    {
        public ApplicationResponseList(bool success, string message, IEnumerable<T> data)
            : base(success, message)
        {
            Data = data;
        }

        public IEnumerable<T> Data { get; private set; }
    }
}

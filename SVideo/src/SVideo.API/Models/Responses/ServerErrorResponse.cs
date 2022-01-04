﻿namespace SVideo.API.Models.Responses
{
    /// <summary>
    /// Server Error Response
    /// </summary>
    public class ServerErrorResponse
    {
        /// Error property
        public string Error { get; private set; }
        /// innerException property
        public string InnerException { get; private set; }
        /// stackTrace property
        public string StackTrace { get; private set; }

        /// Constructor default
        public ServerErrorResponse(string error, string innerException, string stackTrace)
        {
            Error = error;
            InnerException = innerException;
            StackTrace = stackTrace;
        }
    }
}

using System;
using System.Collections.Generic;

namespace SVideo.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException(List<string> failures)
        {
            Failures = failures;
        }

        public List<string> Failures { get; }
    }
}
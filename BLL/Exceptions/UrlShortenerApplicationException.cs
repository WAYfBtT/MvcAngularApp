using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BLL.Exceptions
{
    public class UrlShortenerApplicationException : Exception
    {
        public Dictionary<string, List<string>> Errors { get; protected set; }
        public UrlShortenerApplicationException(Dictionary<string, List<string>> errors)
        {
            Errors = errors;
        }

        public UrlShortenerApplicationException(string message) : base(message)
        {
        }

        public UrlShortenerApplicationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UrlShortenerApplicationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}

using System;
using System.Runtime.Serialization;

namespace BLL.Exceptions
{
    public class UrlShortenerApplicationException : Exception
    {
        public UrlShortenerApplicationException()
        {
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

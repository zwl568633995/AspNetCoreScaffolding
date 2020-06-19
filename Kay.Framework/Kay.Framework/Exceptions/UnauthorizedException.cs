using Kay.Framework.Exceptions.Common;
using System;
using System.Net;
using System.Runtime.Serialization;

namespace Kay.Framework.Exceptions
{
    public class UnauthorizedException : BaseException
    {
        public override HttpStatusCode HttpStatusCode { get; set; } = HttpStatusCode.OK;

        public UnauthorizedException(string errorMessage, int errorNumber)
            : base(errorMessage, errorNumber)
        {

        }

        public UnauthorizedException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
        }

        public UnauthorizedException(string errorMessage)
            : base(errorMessage)
        {
        }

        public UnauthorizedException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}

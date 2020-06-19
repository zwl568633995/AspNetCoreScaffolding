using Kay.Framework.Exceptions.Common;
using System;
using System.Net;
using System.Runtime.Serialization;

namespace Kay.Framework.Exceptions
{
    public class ForbiddenException : BaseException
    {
        public override HttpStatusCode HttpStatusCode { get; set; } = HttpStatusCode.OK;

        public ForbiddenException(string errorMessage, int errorNumber)
            : base(errorMessage, errorNumber)
        {

        }

        public ForbiddenException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {
        }

        public ForbiddenException(string errorMessage)
            : base(errorMessage)
        {
        }

        public ForbiddenException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}

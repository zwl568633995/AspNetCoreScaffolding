using Kay.Framework.Exceptions.Common;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Runtime.Serialization;


namespace Kay.Framework.Exceptions
{
    public class CustomerException : BaseException
    {
        public override HttpStatusCode HttpStatusCode { get; set; } = HttpStatusCode.OK;

        public string ErrorDetails { get; set; }

        public CustomerException(string errorMessage)
            : base(errorMessage)
        {

        }

        public CustomerException(string errorMessage, int errorNumber)
            : base(errorMessage, errorNumber)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorCode">MissingContentType</param>
        /// <param name="errorMessage">Content-Type {type} is unsupported.</param>
        /// <param name="errorDetails">不支持 Content-Type 指定的类型。</param>
        /// <param name="innerException"></param>
        /// <param name="logLevel"></param>
        public CustomerException(
            string errorCode = null,
            string errorMessage = null,
            string errorDetails = null,
            Exception innerException = null,
            LogLevel logLevel = LogLevel.Warning)
            : base(errorMessage, innerException)
        {
            ErrorCode = errorCode;
            ErrorDetails = errorDetails;
            LogLevel = logLevel;
        }

        public CustomerException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }
    }
}
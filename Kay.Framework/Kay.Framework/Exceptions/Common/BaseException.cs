using Kay.Framework.Logging;
using System;
using System.Net;
using System.Runtime.Serialization;
using Microsoft.Extensions.Logging;

namespace Kay.Framework.Exceptions.Common
{
     public class BaseException:
     Exception,
        IErrorCode,
        IErrorNumber,
        IExceptionLogLevel,
        IExceptionHttpStatusCode
    {
        public BaseException()
        {

        }

        public BaseException(string errorMessage, int errorNumber = DefaultErrorNumber)
            : base(errorMessage)
        {
            ErrorNumber = errorNumber;
        }

        public BaseException(string errorMessage)
            : base(errorMessage)
        {

        }

        public BaseException(string errorMessage, int errorNumber = DefaultErrorNumber,
            Exception innerException = null)
            : base(errorMessage, innerException)
        {
            ErrorNumber = errorNumber;
        }

        public BaseException(string errorMessage, Exception innerException)
            : base(errorMessage, innerException)
        {

        }

        public BaseException(SerializationInfo serializationInfo, StreamingContext context)
            : base(serializationInfo, context)
        {

        }

        #region

        /// <summary>
        /// 默认赋值LogLevel.Warning
        /// </summary>
        public virtual LogLevel LogLevel { get; set; } = LogLevel.Warning;

        /// <summary>
        /// 默认赋值400错误
        /// </summary>
        public virtual HttpStatusCode HttpStatusCode { get; set; } = HttpStatusCode.BadRequest;

        /// <summary>
        /// 业务异常代码
        /// </summary>
        public virtual string ErrorCode { get; set; }

        #endregion

        public int ErrorNumber { get; set; } = DefaultErrorNumber;

        /// <summary>
        /// 默认错误码
        /// </summary>
        public const int DefaultErrorNumber = 1;
    }
}
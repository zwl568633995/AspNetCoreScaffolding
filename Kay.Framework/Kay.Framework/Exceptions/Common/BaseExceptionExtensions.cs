using Microsoft.Extensions.Logging;
using Kay.Framework.Logging;
using System;
using System.Net;

namespace Kay.Framework.Exceptions.Common
{
    public static class CommonExceptionExtensions
    {
        /// <summary>
        /// 根据异常获取LogLevel，默认是LogLevel.Error
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="defaultLevel"></param>
        /// <returns></returns>
        public static LogLevel GetLogLevel(this Exception exception, LogLevel defaultLevel = LogLevel.Error)
        {
            return (exception as IExceptionLogLevel)?.LogLevel ?? defaultLevel;
        }

        /// <summary>
        /// 根据异常获取HttpStatusCode，默认是 500  HttpStatusCode.InternalServerError
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="defaultCode"></param>
        /// <returns></returns>
        public static HttpStatusCode GetHttpStatusCode(this Exception exception,
            HttpStatusCode defaultCode = HttpStatusCode.InternalServerError)
        {
            return (exception as IExceptionHttpStatusCode)?.HttpStatusCode ?? defaultCode;
        }

        /// <summary>
        /// 根据异常获取获取错误码
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="errorCode"></param>
        /// <returns></returns>
        public static string GetErrorCode(this Exception exception, string errorCode = "")
        {
            return (exception as IErrorCode)?.ErrorCode ?? errorCode;
        }

        /// <summary>
        /// 根据异常获取获取错误int值，默认是1
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="errorNumber">默认是1</param>
        /// <returns></returns>
        public static int GetErrorNumber(this Exception exception, int errorNumber = 1)
        {
            return (exception as IErrorNumber)?.ErrorNumber ?? errorNumber;
        }
    }
}
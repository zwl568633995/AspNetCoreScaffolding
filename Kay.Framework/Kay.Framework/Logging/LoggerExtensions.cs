using Kay.Framework.Exceptions.Common;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kay.Framework.Logging
{
    public static class LoggerExtensions
    {
        public static void LogWithLevel(this ILogger logger, LogLevel logLevel, string message)
        {
            switch (logLevel)
            {
                case LogLevel.Critical:
                    logger.LogCritical(message);
                    break;
                case LogLevel.Error:
                    logger.LogError(message);
                    break;
                case LogLevel.Warning:
                    logger.LogWarning(message);
                    break;
                case LogLevel.Information:
                    logger.LogInformation(message);
                    break;
                case LogLevel.Trace:
                    logger.LogTrace(message);
                    break;
                default:
                    logger.LogDebug(message);
                    break;
            }
        }

        public static void LogWithLevel(this ILogger logger, LogLevel logLevel, string message, Exception exception)
        {
            switch (logLevel)
            {
                case LogLevel.Critical:
                    logger.LogCritical(exception, message);
                    break;
                case LogLevel.Error:
                    logger.LogError(exception, message);
                    break;
                case LogLevel.Warning:
                    logger.LogWarning(exception, message);
                    break;
                case LogLevel.Information:
                    logger.LogInformation(exception, message);
                    break;
                case LogLevel.Trace:
                    logger.LogTrace(exception, message);
                    break;
                default:
                    logger.LogDebug(exception, message);
                    break;
            }
        }

        public static void LogException(this ILogger logger, Exception ex, LogLevel? level = null)
        {
            var selectedLevel = level ?? ex.GetLogLevel();
            logger.LogWithLevel(selectedLevel, ex.Message, ex);
        }

        public static void LogException(this ILogger logger,
            Exception ex,
            string message,
            LogLevel? level = null)
        {
            var selectedLevel = level ?? ex.GetLogLevel();
            logger.LogWithLevel(selectedLevel, message, ex);
        }
    }
}

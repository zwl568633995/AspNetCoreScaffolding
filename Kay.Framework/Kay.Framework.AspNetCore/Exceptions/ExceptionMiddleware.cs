using Kay.Framework.AspNetCore.Http;
using Kay.Framework.Exceptions;
using Kay.Framework.Exceptions.Common;
using Kay.Framework.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kay.Framework.AspNetCore.Exceptions
{
    public class ExceptionMiddleware:BaseMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                if (context.Response.HasStarted)
                {
                    _logger.LogWarning("触发了异常, 但是Response HasStarted!");
                    throw;
                }

                await HandlerException(context, e);
            }
        }

        private async Task HandlerException(HttpContext httpContext, Exception exception)
        {
            var error = Convert(exception);
            var errorData = new NalongApiResponse(exception.GetErrorNumber(), error.ErrorMessage, error);
            //异常-驼峰命名
            var errorResponse = JsonConvert.SerializeObject(errorData, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            }
            );
            httpContext.Items["nl-items-errorCode"] = exception.GetErrorCode();
            httpContext.Items["nl-items-middleware"] = "ExceptionMiddleware";
            _logger.LogException(exception, errorResponse);
            //清除掉errorCode 污染审计中间件
            httpContext.Items["nl-items-errorCode"] = "";
            // httpContext.Response.StatusCode = (int)exception.GetHttpStatusCode();
            // 这里强写200
            httpContext.Response.StatusCode = 200;
            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsync(errorResponse);
        }

        /// <summary>
        /// todo 未实现的异常
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        private ErrorResponse Convert(Exception exception)
        {
            if (exception is UnauthorizedException)
            {
                var ex = exception as UnauthorizedException;
                return new ErrorResponse(
                    ex.Message,
                    ex.InnerException?.Message,
                    "Unauthorized");
            }

            if (exception is NotFoundException)
            {
                var ex = exception as NotFoundException;
                return new ErrorResponse(
                    ex.Message,
                    ex.InnerException?.Message,
                    "NotFound");
            }

            if (exception is ForbiddenException)
            {
                var ex = exception as ForbiddenException;
                return new ErrorResponse(
                    ex.Message,
                    ex.InnerException?.Message,
                    "Forbidden");
            }

            if (exception is NotImplementedException)
            {
                return new ErrorResponse(
                    exception.Message,
                    exception.InnerException?.Message,
                    "NotImplemented");
            }

            if (exception is NotSupportedException)
            {
                return new ErrorResponse(
                    exception.Message,
                    exception.InnerException?.Message,
                    "NotSupported");
            }

            #region ErrorCode 需要自定义

            if (exception is ValidationException)
            {
                var ex = exception as ValidationException;
                return new ErrorResponse(
                    ex.Message,
                    ex.InnerException?.Message,
                    "BadRequestObject",
                    ex.ValidationErrors?.ToArray());
            }

            if (exception is CustomerException)
            {
                var ex = exception as CustomerException;
                return new ErrorResponse(
                    ex.Message,
                    ex.ErrorDetails,
                    ex.ErrorCode);
            }

            #endregion

            return new ErrorResponse(
                exception.Message,
                exception.InnerException?.Message,
                "InternalServerError");
        }
    }
}
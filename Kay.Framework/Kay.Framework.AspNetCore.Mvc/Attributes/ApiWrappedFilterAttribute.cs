using Kay.Framework.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kay.Framework.AspNetCore.Mvc.Attributes
{
    /// <summary>
    /// 将原始结果进行返回，不包装成code data msg格式
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class IgnoreWrappedApiAttribute : Attribute
    {
    }

    public class ApiWrappedFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null)
            {
                return;
            }

            var ignoreWrapper = IsIgnoreWrappedApi(context);
            if (!ignoreWrapper)
            {
                var wrapperData = GetWrapperData(context);
                var result = new NalongApiResponse(0, string.Empty, wrapperData);
                context.Result = new OkObjectResult(result);
            }
        }


        private static readonly ConcurrentDictionary<string, bool> IgnoreWrapperApiMapper
            = new ConcurrentDictionary<string, bool>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// 是否是需要包装成code data msg的api
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private static bool IsIgnoreWrappedApi(ActionExecutedContext context)
        {
            var actionDescriptor = (ControllerActionDescriptor)context.ActionDescriptor;
            var actionMethod = actionDescriptor.MethodInfo;
            var requestMethod = context.HttpContext.Request.Method;
            var controllerName = actionDescriptor.ControllerName;
            var actionName = actionDescriptor.ActionName;
            var parameters = actionDescriptor.Parameters.Where(m => m != null).Select(m => m.Name).ToArray();
            var mapperKey = ($"[{actionMethod.Name}]:{requestMethod}:{controllerName}/{actionName}" +
                             $":{string.Join("-", parameters)}").ToLowerInvariant();
            if (IgnoreWrapperApiMapper.TryGetValue(mapperKey, out var result))
            {
                return result;
            }

            //特性获取
            var attributeList = new List<object>();
            var controllerAttrs = actionMethod.DeclaringType?.GetCustomAttributes(true);
            var actionAttrs = actionMethod.GetCustomAttributes(true);
            attributeList.AddRange(controllerAttrs);
            attributeList.AddRange(actionAttrs);
            var attributeArray = attributeList.Where(m => m != null).Select(m => m).ToArray();
            //如果有NalongIgnoreWrappedApi特性则无需理会，使用原始返回值
            if (attributeArray.OfType<IgnoreWrappedApiAttribute>().Any())
            {
                IgnoreWrapperApiMapper.TryAdd(mapperKey, true);
                return true;
            }

            IgnoreWrapperApiMapper.TryAdd(mapperKey, false);
            return false;
        }

        /// <summary>
        /// 获取待包装的数据
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private static object GetWrapperData(ActionExecutedContext context)
        {
            if (context.Result is ObjectResult objectResult) return objectResult.Value;
            if (context.Result is ContentResult contentResult) return contentResult.Content;
            if (context.Result is JsonResult jsonResult) return jsonResult.Value;
            var error = $"Can not wrapper the contextResult of {context.Result.GetType().Name}";
            throw new ValidationException(error);
        }
    }
}

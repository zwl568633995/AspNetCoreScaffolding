using JWT;
using JWT.Builder;
using Kay.Framework.AspNetCore.Mvc.Attributes;
using Kay.Framework.Exceptions;
using Kay.Framework.Utility.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using AuthorizeAttribute = Kay.Framework.AspNetCore.Mvc.Attributes.AuthorizeAttribute;

namespace Kay.Framework.Authorization
{
    public class AuthorizationFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!(context.ActionDescriptor is ControllerActionDescriptor descriptor))
            {
                return;
            }

            var isClassTarget = Attribute.GetCustomAttribute(descriptor.ControllerTypeInfo, typeof(AuthorizeAttribute)) != null;
            var isMethodTarget = Attribute.GetCustomAttribute(descriptor.MethodInfo, typeof(AuthorizeAttribute)) != null;
            var isAnonymous = Attribute.GetCustomAttribute(descriptor.MethodInfo, typeof(AllowAnonymousAttribute)) != null;
            if (!isClassTarget && !isMethodTarget)
            {
                return;
            }
            if (isClassTarget && isAnonymous)
            {
                return;
            }

            var headers = context.HttpContext.Request.Headers["Authorization"];
            if (headers.Count == 0 || headers.ToString().TrimSafe().IsNullOrEmpty())
            {
                throw new UnauthorizedException("没有token信息", 1011);
            }
            var token = headers.ToString().TrimSafe().Replace("Bearer ", string.Empty);
            IDictionary<string, object> payload;
            try
            {
                payload = new JwtBuilder().Decode<IDictionary<string, object>>(token);
            }
            catch
            {
                throw new UnauthorizedException("token格式错误", 1013);
            }
            if (payload["exp"].ToDouble() < UnixEpoch.GetSecondsSince(DateTimeOffset.UtcNow))
            {
                throw new UnauthorizedException("token已过期", 1012);
            }
            //var detail = new AccountDetail
            //{
            //    Id = payload[AccountConstant.AccountId].ToString(),
            //    Account = payload[AccountConstant.Account].ToString(),
            //    NickName = payload[AccountConstant.NickName].ToString(),
            //    Type = payload[AccountConstant.AccountType].ToString().ToEnum<AccountType>(),
            //    BeginTime = payload[AccountConstant.BeginTime].ToString().ToDateTimeOrDefault(),
            //    EndTime = payload[AccountConstant.EndTime].ToString().ToDateTimeOrDefault(),
            //    Password = string.Empty,
            //    AbsKey = payload[AccountConstant.AbsKey].ToString().ToLongOrDefault(0),
            //    LockStatus = payload[AccountConstant.LockStatus].ToString().ToIntOrDefault(0)
            //};
            //context.HttpContext.Items[nameof(AccountDetail)] = detail;
        }
    }
}


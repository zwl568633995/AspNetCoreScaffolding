using JWT;
using JWT.Builder;
using Kay.Boilerplate.ApplicationService.Dto;
using Kay.Boilerplate.ApplicationService.IAppService;
using Kay.Framework.AspNetCore.Mvc.Attributes;
using Kay.Framework.Exceptions;
using Kay.Framework.Redis;
using Kay.Framework.Utility.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kay.Boilerplate.Application.Service.Http.Filter
{
    public class AuthorizationFilter : IAuthorizationFilter
    {
        private readonly RedisCliHelper _client;
        private readonly IUserAppService _userAppService;
        private readonly IConfiguration _configuration;
        public AuthorizationFilter(RedisCliHelper client, IUserAppService userAppService, IConfiguration configuration)
        {
            _client = client;
            _userAppService = userAppService;
            _configuration = configuration;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!(context.ActionDescriptor is ControllerActionDescriptor descriptor))
            {
                return;
            }

            var isClassTarget = Attribute.GetCustomAttribute(descriptor.ControllerTypeInfo, typeof(NeedAuthorizeAttribute)) != null;
            var isMethodTarget = Attribute.GetCustomAttribute(descriptor.MethodInfo, typeof(NeedAuthorizeAttribute)) != null;
            var isAnonymous = Attribute.GetCustomAttribute(descriptor.MethodInfo, typeof(AllowAnonymousAttribute)) != null;
            if (!isClassTarget && !isMethodTarget)
            {
                return;
            }
            if (isClassTarget && isAnonymous)
            {
                return;
            }

            var token = context.HttpContext.Request.Headers["token"];
            if (token.Count == 0 || token.ToString().TrimSafe().IsNullOrEmpty())
            {
                throw new UnauthorizedException("没有token信息");
            }

            // 获取 token 中的 user id
            string username = _client.Get(token);
            if (username.IsNotNullOrEmpty())
            {
                //读取Redis时间配置
                long resetTime = _configuration.GetSection("Redis").GetValue<long>("RetTime");
                int expireTime = _configuration.GetSection("Redis").GetValue<int>("ExpireTime");
                long tokeBirthTime = 0;
                long.TryParse(_client.Get(token + username), out tokeBirthTime);
                long diff = DateTime.Now.GetJsTimestamp() - tokeBirthTime;
                //重新设置Redis中的token过期时间
                if (diff > 1000 * 100)
                {
                    _client.Expire(username, 60 * 10);
                    _client.Expire(token, 60 * 10);
                    long newBirthTime = DateTime.Now.GetJsTimestamp();
                    _client.Set(token + username, newBirthTime.ToString());
                }

                UserDto user = _userAppService.GetByUserName(username);
                if (user == null)
                {
                    throw new NotFoundException("用户不存在，请重新登录");
                }
            }
            else
            {
                throw new UnauthorizedException("请先登录");
            }
        }
    }
}

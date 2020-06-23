using Kay.Boilerplate.ApplicationService.IAppService;
using Kay.Framework.Authorization;
using Kay.Framework.Redis;
using Kay.Framework.Utility.Extensions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kay.Boilerplate.ApplicationService.AppService
{
    public class AuthAppService : Kay.Framework.Application.Application.Services.AppService, IAuthAppService
    {
        private readonly RedisCliHelper _redisCliHelper;
        private readonly IConfiguration _configuration;
        public AuthAppService(
           IServiceProvider serviceProvider,
           RedisCliHelper client,
           IConfiguration configuration)
           : base(serviceProvider)
        {
            _redisCliHelper = client;
            _configuration = configuration;
        }
        public string GetToken(string username, string pwd)
        {
            string token = _redisCliHelper.Get(username);
            //读取Redis时间配置
            long resetTime = _configuration.GetSection("Redis").GetValue<long>("RetTime");
            int expireTime = _configuration.GetSection("Redis").GetValue<int>("ExpireTime");
            if (token.IsNotNullOrEmpty())
            {
                long tokeBirthTime = 0;
                long.TryParse(_redisCliHelper.Get(token + username), out tokeBirthTime);
                long diff = DateTime.Now.GetJsTimestamp() - tokeBirthTime;
                //重新设置Redis中的token过期时间  大于了100s重置redis时间
                if (diff > resetTime)
                {
                    _redisCliHelper.Expire(username, expireTime);
                    _redisCliHelper.Expire(token, expireTime);
                    long newBirthTime = DateTime.Now.GetJsTimestamp();
                    _redisCliHelper.Set(token + username, newBirthTime.ToString());
                }
            }
            else
            {
                token = TokenHelper.GetToken(username, pwd);
                _redisCliHelper.Set(username, token, expireTime);
                _redisCliHelper.Set(token, username, expireTime);
                long currentTime = DateTime.Now.GetJsTimestamp();
                _redisCliHelper.Set(token + username, currentTime.ToString());
            }

            return token;
        }
    }
}

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Kay.Framework.Redis
{
    public class RedisCliHelper
    {
        private CSRedis.CSRedisClient csredis = null;

        public RedisCliHelper(string redisCon)
        {
            csredis = new CSRedis.CSRedisClient(redisCon);
            //初始化 RedisHelper
            RedisHelper.Initialization(csredis);
        }

        public bool Set(string key, string value, TimeSpan expoireseconds)
        {
            return RedisHelper.Set(key, value, expoireseconds);
        }

        public bool Set(string key, string value)
        {
            return RedisHelper.Set(key, value);
        }

        public bool Set(string key, string value, int seconds)
        {
            return RedisHelper.Set(key, value, seconds);
        }

        public string Get(string key)
        {
            return RedisHelper.Get<string>(key);
        }

        public bool Expire(string key, int seconds)
        {
            return RedisHelper.Expire(key, seconds);
        }
    }
}

using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Kay.Framework.Extensions
{
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// 根据key获取Bool类型的value值
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue">若获取不到，默认是false</param>
        /// <returns></returns>
        public static bool GetBoolValue(this IConfiguration configuration, string key, bool defaultValue = false)
        {
            return configuration.GetValue<bool>(key, defaultValue);
        }

        /// <summary>
        /// 根据key获取string类型的value值
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue">若获取不到，默认是""</param>
        /// <returns></returns>
        public static string GetStringValue(this IConfiguration configuration, string key, string defaultValue = "")
        {
            return configuration.GetValue<string>(key, defaultValue);
        }

        /// <summary>
        /// 根据key获取long类型的value值
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue">若获取不到，默认是0</param>
        /// <returns></returns>
        public static long GetLongValue(this IConfiguration configuration, string key, long defaultValue = 0)
        {
            return configuration.GetValue<long>(key, defaultValue);
        }

        /// <summary>
        /// 根据key获取int类型的value值
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue">若获取不到，默认是0</param>
        /// <returns></returns>
        public static int GetIntValue(this IConfiguration configuration, string key, int defaultValue = 0)
        {
            return configuration.GetValue<int>(key, defaultValue);
        }

        /// <summary>
        /// 根据key从json字符串反序列化成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="configuration"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetJsonValue<T>(this IConfiguration configuration, string key)
        {
            var jsonStr = configuration.GetStringValue(key);
            return JsonConvert.DeserializeObject<T>(jsonStr);
        }
    }
}
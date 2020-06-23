using System;
using System.Collections.Generic;
using System.Text;

namespace Kay.Framework.Utility.Extensions
{
    public static class TimespanExtensions
    {
        /// <summary>
        ///     把时间转换为javascript所使用的时间
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static long GetJsTimestamp(this DateTime dateTime)
        {
            var startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)); // 当地时区
            var timeStamp = (long)(dateTime - startTime).TotalMilliseconds; // 相差毫秒数
            return timeStamp;
        }
    }
}

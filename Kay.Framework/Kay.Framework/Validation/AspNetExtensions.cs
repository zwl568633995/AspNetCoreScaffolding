using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Kay.Framework.Validation
{
    public static class AspNetExtensions
    {
        /// <summary>
        /// 根据类型T获取Service
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <returns></returns>
        public static T GetService<T>(this ValidationContext context) where T : class
        {
            return context.GetRequiredService<T>();
        }
    }
}

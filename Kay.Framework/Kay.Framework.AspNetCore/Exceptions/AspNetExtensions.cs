using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kay.Framework.AspNetCore.Exceptions
{
    public static class AspNetExtensions
    {
        /// <summary>
        /// 纳龙异常中间件，不需要时可以不引用，运行时没有必要添加配置是否启用
        /// </summary>
        /// <param name="app"></param>
        public static void UseException(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}

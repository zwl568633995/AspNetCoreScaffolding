using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kay.Framework.AspNetCore.Auditing
{
    public static class AspNetExtensions
    {
        /// <summary>
        /// 审计中间件 return $"Auditing Response {text}"; 通过配置 disable_auditing = true 关闭审计
        /// </summary>
        /// <param name="app"></param>
        public static void UseAuditing(this IApplicationBuilder app)
        {
            app.UseMiddleware<AuditingMiddleware>();
        }
    }
}

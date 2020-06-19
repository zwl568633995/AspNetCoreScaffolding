using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kay.Framework.AspNetCore.Mvc.MiniProfiler
{
    public static class MiniProfilerExtensions
    {
        public static void AddEfMiniProfiler(this IServiceCollection services)
        {
            services
                .AddMiniProfiler()
                .AddEntityFramework();
        }

        public static void UseEfMiniProfiler(this IApplicationBuilder app)
        {
            app.UseMiniProfiler();
        }
    }
}

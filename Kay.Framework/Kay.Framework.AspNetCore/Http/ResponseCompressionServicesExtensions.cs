using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kay.Framework.AspNetCore.Http
{
    public static class ResponseCompressionServicesExtensions
    {
        /// <summary>
        /// 封装ResponseCompression
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddResponseCompression(
            this IServiceCollection services)
        {
            return services.AddResponseCompression(options => { options.Providers.Add<GzipCompressionProvider>(); });
        }
    }
}

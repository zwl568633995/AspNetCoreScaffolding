using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kay.Framework.AspNetCore.Http
{
    public static class ResponseCompressionBuilderExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseResponseCompression(
            this IApplicationBuilder builder)
        {
            return builder.UseResponseCompression();
        }
    }
}

using Kay.Framework.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StackExchange.Profiling;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kay.Framework.AspNetCore.Auditing
{
    public class AuditingMiddleware : BaseMiddleware
    {
        private readonly ILogger _logger;
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public AuditingMiddleware(
            RequestDelegate next,
            ILogger<AuditingMiddleware> logger,
            IConfiguration configuration)
        {
            _next = next;
            _logger = logger;
            _configuration = configuration;
        }

        /// <summary>
        /// 审计中间件
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            var auditing = !_configuration.GetBoolValue("base.disable_auditing");
            var isHealthCheck = IsHealthCheck(context);
            if (auditing && !isHealthCheck)
            {
                var start = Stopwatch.GetTimestamp();
                var originalBodyStream = context.Response.Body;
                using (var responseBody = new MemoryStream())
                {
                    context.Response.Body = responseBody;
                    await Next(context);
                    var elapsedMs = GetElapsedMilliseconds(start, Stopwatch.GetTimestamp());
                    context.Items["nl-items-elapsed"] = $"{elapsedMs} ms";
                    context.Items["nl-items-statusCode"] = context.Response.StatusCode;
                    context.Items["nl-items-middleware"] = "AuditingMiddleware";
                    _logger.LogInformation(await FormatResponse(context.Response));
                    await responseBody.CopyToAsync(originalBodyStream);
                }
            }
            else
            {
                await Next(context);
            }
        }

        private async Task Next(HttpContext context)
        {
            var miniProfiler = !_configuration.GetBoolValue("base.disable_auditing_MiniProfiler");
            if (miniProfiler)
            {
                var name = "NL.Sql.MiniProfiler";
                using (MiniProfiler.Current.Step(name))
                {
                    await _next(context);
                }

                var profiler = MiniProfiler.Current?.Head?.Children?.FirstOrDefault(a => a.Name == name);
                var child = profiler?.Children?.FirstOrDefault();
                if (child != null && !string.IsNullOrEmpty(child.CustomTimingsJson))
                {
                    _logger.LogInformation(child.CustomTimingsJson);
                }
            }
            else
            {
                await _next(context);
            }
        }

        private bool IsHealthCheck(HttpContext context)
        {
            var healthPath = _configuration.GetStringValue("base.health_path", "/health");
            var isHealthCheck = context.Request.Path.StartsWithSegments(new PathString(healthPath));
            return isHealthCheck;
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var text = "";
            if (response.Headers.Any(a => a.Key == "Content-Encoding" && a.Value.Contains("gzip")))
            {
                var gZipStream = new GZipStream(response.Body, CompressionMode.Decompress);
                using (var outputStream = new MemoryStream())
                {
                    gZipStream.CopyTo(outputStream);
                    var outputBytes = outputStream.ToArray();
                    text = Encoding.UTF8.GetString(outputBytes);
                }
            }
            else
            {
                text = await new StreamReader(response.Body).ReadToEndAsync();
            }

            response.Body.Seek(0, SeekOrigin.Begin);
            return $"Auditing Response {text}";
        }

        private double GetElapsedMilliseconds(long start, long stop)
        {
            return (stop - start) * 1000 / (double)Stopwatch.Frequency;
        }
    }
}

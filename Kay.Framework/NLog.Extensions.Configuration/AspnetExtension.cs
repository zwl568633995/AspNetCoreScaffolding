using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog.Web;
using System;
using System.IO;

namespace NLog.Extensions.Configuration
{
    public static class AspNetExtensions
    {
        /// <summary>
        /// 从Configuration中获取NLog的String配置，转化成StringReader，默认Key:nlog。如果只想通过本地nlog.config文件配置，不需要AddNalongNLog
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="configuration"></param>
        /// <param name="topic">日志topic</param>
        /// <param name="nlogKey">Configuration中nlog的配置key值</param>
        public static void AddNLog(this IServiceCollection serviceCollection,
            IConfiguration configuration,
            string topic = "")
        {
            var filename = configuration.GetValue<string>("nlogFileName");
            if (string.IsNullOrEmpty(filename))
            {
                throw new Exception($"无法从配置中获取nlogFileName的值");
            }

            //string config = System.IO.
            //var xmlStream = new System.IO.StringReader(config);
            string filePath = System.AppDomain.CurrentDomain.BaseDirectory + filename;
            var xmlReader = System.Xml.XmlReader.Create(new FileStream(filePath, FileMode.Open));
            LogManager.Configuration = new Config.XmlLoggingConfiguration(xmlReader, null);
            if (!string.IsNullOrEmpty(topic))
            {
                GlobalDiagnosticsContext.Set("topic", topic);
            }
            else
            {
                GlobalDiagnosticsContext.Set("topic", "其他");
            }
        }

        /// <summary>
        /// 封装UseNLog
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IWebHostBuilder UseNLog(this IWebHostBuilder app)
        {
            app.UseNLog();
            return app;
        }
    }
}

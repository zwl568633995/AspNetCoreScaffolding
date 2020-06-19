using Kay.Framework.ObjectMapping.Abstractions.Config;
using Kay.Framework.ObjectMapping.Abstractions.TypeConverter;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kay.Framework.ObjectMapping.Abstractions.DependencyInjection
{
    public static class AspNetExtensions
    {
        /// <summary>
        /// 添加映射配置
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="configs"></param>
        public static void AddTypeConverterConfigs(
            this IServiceCollection serviceCollection,
            params ITypeConverterConfig[] configs)
        {
            var config = new ObjectMappingConfig
            {
                TypeConverterConfigs = configs?.ToList()
            };
            serviceCollection.AddSingleton(typeof(IObjectMappingConfig), config);
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Kay.Framework.DependencyInjection;
using Kay.Framework.RegisterInterfaces;
using System.Linq;

namespace Kay.Framework.AspNetCore.Mvc.Containers
{
    public static class ServiceContainer
    {
        /// <summary>
        /// 注册assemblies集合中继承自INalongApiService的类
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        public static void AddNalongApiService(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddScoped<IApiService>(assemblies);
        }

        /// <summary>
        /// 注册assemblies集合中继承自INalongAppService的类
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        public static void AddNalongAppService(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddScoped<IAppService>(assemblies);
        }

        /// <summary>
        ///  注册assemblies集合中继承自NalongDomainService的类，注意自定义DomainService不需要新增接口
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        public static void AddNalongDomainService(this IServiceCollection services, params Assembly[] assemblies)
        {
            var list = new List<Type>();
            foreach (var assembly in assemblies)
            {
                list = list.Union(assembly.GetTypes().ToList()).ToList();
            }

            var mappers = AspNetExtensions.GetServicesMapper<IDomainService>(list);
            foreach (var item in mappers)
            {
                services.AddScoped(item.Key);
            }
        }

        ///// <summary>
        ///// 注册assemblies集合中继承自INalongDomainEvent的类
        ///// </summary>
        ///// <param name="services"></param>
        ///// <param name="assemblies"></param>
        //public static void AddNalongDomainEvent(this IServiceCollection services, params Assembly[] assemblies)
        //{
        //    services.AddScoped<IDomainEvent>(assemblies);
        //}

        /// <summary>
        /// 注册assemblies集合中继承自INalongPlugInService的类
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        public static void AddNalongPlugInService(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddSingleton<IPluginService>(assemblies);

        }

        /// <summary>
        /// 注册assemblies集合中继承自INalongConfig的类
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        public static void AddNalongConfig(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddSingleton<IConfig>(assemblies);
        }
    }
}

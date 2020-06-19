using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Kay.Framework.DependencyInjection
{
    public static class AspNetExtensions
    {
        public static IServiceCollection AddSingleton<T>(this IServiceCollection services,
           params Assembly[] assemblies)
           where T : class
        {
            Add<T>(services, assemblies, ServiceLifetime.Singleton);
            return services;
        }

        public static IServiceCollection AddScoped<T>(
            this IServiceCollection services,
            params Assembly[] assemblies)
            where T : class
        {
            Add<T>(services, assemblies, ServiceLifetime.Scoped);
            return services;
        }

        public static IServiceCollection AddTransient<T>(
            this IServiceCollection services,
            params Assembly[] assemblies)
            where T : class
        {
            Add<T>(services, assemblies, ServiceLifetime.Transient);
            return services;
        }

        private static void Add<T>(IServiceCollection services, Assembly[] assemblies,
            ServiceLifetime lifetime = ServiceLifetime.Transient) where T : class
        {
            var assemblyTypesList = new List<Type>();
            foreach (var types in assemblies.Select(m => m.GetTypes()))
            {
                assemblyTypesList.AddRange(types);
            }

            var mappers = GetServicesMapper<T>(assemblyTypesList);
            foreach (var item in mappers)
            {
                foreach (var typeArray in item.Value)
                {
                    switch (lifetime)
                    {
                        case ServiceLifetime.Scoped:
                            {
                                services.AddScoped(typeArray, item.Key);
                                break;
                            }

                        case ServiceLifetime.Singleton:
                            {
                                services.AddSingleton(typeArray, item.Key);
                                break;
                            }

                        case ServiceLifetime.Transient:
                            {
                                services.AddTransient(typeArray, item.Key);
                                break;
                            }

                        default:
                            throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, null);
                    }
                }
            }
        }

        public static Dictionary<Type, Type[]> GetServicesMapper<T>(List<Type> types)
        {
            var typeMapper = new Dictionary<Type, Type[]>();
            var interfaceTypes = types.Where(m => !m.IsAbstract)
                .Where(m => !string.IsNullOrWhiteSpace(m.Namespace))
                .Where(m => m.IsClass)
                .Where(m => m.GetInterfaces().Contains(typeof(T))).ToArray();
            foreach (var item in interfaceTypes)
            {
                var interfaceType = item.GetInterfaces();
                typeMapper.Add(item, interfaceType);
            }
            return typeMapper;
        }
    }
}

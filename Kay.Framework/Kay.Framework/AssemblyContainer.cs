using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Linq;

namespace Kay.Framework
{
    public static class AssemblyContainer
    {
        public static Assembly[] Assemblies;

        public static readonly Type[] Types;

        static AssemblyContainer()
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-us");
            Assemblies = GetAssemblies();
            Types = GetTypesByAssemblies(Assemblies);
        }

        public static Dictionary<Type, Type[]> GetServicesMapper<T>()
        {
            var typeMapper = new Dictionary<Type, Type[]>();
            var interfaceTypes = Types.Where(m => !m.IsAbstract)
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

        #region 私有辅助方法

        /// <summary>
        /// 获取所有的Assemblies
        /// </summary>
        /// <returns></returns>
        private static Assembly[] GetAssemblies()
        {
            var assemblies = new HashSet<Assembly>(AppDomain.CurrentDomain.GetAssemblies());
            return assemblies.ToArray();
        }

        /// <summary>
        /// 获取所有类型
        /// </summary>
        /// <returns></returns>
        private static Type[] GetTypesByAssemblies(IEnumerable<Assembly> assemblies)
        {
            var hashSet = new HashSet<Type>();
            foreach (var types in assemblies.Select(m => m.GetTypes()))
            {
                foreach (var item in types)
                {
                    if (!string.IsNullOrWhiteSpace(item?.Namespace))
                    {
                        hashSet.Add(item);
                    }
                }
            }
            return hashSet.ToArray();
        }

        #endregion 私有辅助方法
    }
}

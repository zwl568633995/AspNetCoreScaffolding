using System;
using System.Collections.Generic;
using System.Text;

namespace Kay.Framework.ObjectMapping.Abstractions
{
    public interface IMapper
    {
        /// <summary>
        /// 从object source 映射到一个新的 TDestination 对象
        /// </summary>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        TDestination Map<TDestination>(object source);

        /// <summary>
        /// 从TSource source 映射到一个新的 TDestination 对象
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        TDestination Map<TSource, TDestination>(TSource source);

        /// <summary>
        /// 从TSource source 映射到一个已存在的 TDestination 对象
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        TDestination Map<TSource, TDestination>(TSource source, TDestination destination);

        /// <summary>
        /// 【TinyMapper使用】
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        void Bind<TSource, TDestination>();
    }
}

using Kay.Framework.ObjectMapping.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using Nelibur.ObjectMapper;

namespace Kay.Framework.ObjectMapping.TinyMapper
{
    public class TinyMapperMapper : IMapper
    {
        public TDestination Map<TDestination>(object source)
        {
            return Nelibur.ObjectMapper.TinyMapper.Map<TDestination>(source);
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return Nelibur.ObjectMapper.TinyMapper.Map<TSource, TDestination>(source);
        }

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            return Nelibur.ObjectMapper.TinyMapper.Map<TSource, TDestination>(source);
        }

        public void Bind<TSource, TDestination>()
        {
            Nelibur.ObjectMapper.TinyMapper.Bind<TSource, TDestination>();
        }
    }
}
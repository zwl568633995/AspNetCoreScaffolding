using System;
using System.Collections.Generic;
using System.Text;

namespace Kay.Framework.ObjectMapping.Abstractions.Attributes.MapperFrom
{
    public class MapsFromAttribute : MapsAttribute
    {
        public MapsFromAttribute(Type sourceType)
        {
            SourceType = sourceType;
        }

        public Type SourceType { get; }
    }
}

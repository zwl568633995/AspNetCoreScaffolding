using System;
using System.Collections.Generic;
using System.Text;

namespace Kay.Framework.ObjectMapping.Abstractions.Attributes.MapperFrom
{
    class MapsFromPropertyAttribute : MapsPropertyAttribute
    {
        public MapsFromPropertyAttribute(Type sourceType, string propertyName)
        {
            SourceType = sourceType;
            PropertyName = propertyName;
        }

        public Type SourceType { get; }

        public string PropertyName { get; }
    }
}
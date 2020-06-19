using System;
using System.Collections.Generic;
using System.Text;

namespace Kay.Framework.ObjectMapping.Abstractions.Attributes.MapperTo
{
    public class MapsToPropertyAttribute : MapsPropertyAttribute
    {
        public MapsToPropertyAttribute(Type targetType, string propertyName)
        {
            TargetType = targetType;
            PropertyName = propertyName;
        }

        public Type TargetType { get; }

        public string PropertyName { get; }
    }
}

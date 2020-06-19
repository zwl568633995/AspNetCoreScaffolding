using System;
using System.Collections.Generic;
using System.Text;

namespace Kay.Framework.ObjectMapping.Abstractions.Attributes.MapperTo
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class IgnoreMapToAttribute : MapsPropertyAttribute
    {
        public IgnoreMapToAttribute(Type targetType)
        {
            TargetType = targetType;
        }

        public Type TargetType { get; }
    }
}

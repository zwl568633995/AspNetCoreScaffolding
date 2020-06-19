using System;
using System.Collections.Generic;
using System.Text;

namespace Kay.Framework.ObjectMapping.Abstractions.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public abstract class MapsPropertyAttribute : Attribute
    {
        internal MapsPropertyAttribute()
        {
        }
    }
}

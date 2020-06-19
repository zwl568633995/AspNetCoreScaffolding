using System;
using System.Collections.Generic;
using System.Text;

namespace Kay.Framework.ObjectMapping.Abstractions.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public abstract class MapsAttribute : Attribute
    {
        internal MapsAttribute()
        {
        }

        public bool ReverseMap { get; set; }
    }
}

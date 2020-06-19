using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Kay.Framework.ObjectMapping.Abstractions.Attributes
{
    public class PropertyMapInfo
    {
        public PropertyInfo[] SourcePropertyInfos { get; set; }
        public Type SourceType { get; set; }
        public PropertyInfo TargetPropertyInfo { get; set; }
        public Type TargetType { get; set; }
        public bool IgnoreMapping { get; set; }
        public bool UseSourceMember { get; set; }
    }
}

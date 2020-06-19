using Kay.Framework.ObjectMapping.Abstractions.TypeConverter;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kay.Framework.ObjectMapping.Abstractions.Config
{
    public class ObjectMappingConfig : IObjectMappingConfig
    {
        public List<ITypeConverterConfig> TypeConverterConfigs { get; set; } =
            new List<ITypeConverterConfig>();
    }
}

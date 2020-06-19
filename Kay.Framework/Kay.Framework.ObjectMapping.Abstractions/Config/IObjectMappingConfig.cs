using Kay.Framework.ObjectMapping.Abstractions.TypeConverter;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kay.Framework.ObjectMapping.Abstractions.Config
{
    public interface IObjectMappingConfig
    {
        List<ITypeConverterConfig> TypeConverterConfigs { get; set; }
    }
}

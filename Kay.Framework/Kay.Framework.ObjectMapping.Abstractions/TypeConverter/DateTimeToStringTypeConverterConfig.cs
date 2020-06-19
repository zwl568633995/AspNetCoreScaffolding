using System;
using System.Collections.Generic;
using System.Text;

namespace Kay.Framework.ObjectMapping.Abstractions.TypeConverter
{
    public class DateTimeToStringTypeConverterConfig : ITypeConverterConfig
    {
        public string DateTimeFormat { get; set; }
        public DateTime MinValue { get; set; }

        public string MinValueString { get; set; }

        public DateTimeToStringTypeConverterConfig(
            string dateTimeFormat = "yyyy-MM-dd HH:mm:ss",
            string minValueString = "1800-01-01 00:00:00")
        {
            DateTimeFormat = dateTimeFormat;
            MinValueString = minValueString;
            MinValue = DateTime.Parse(MinValueString);
        }
    }
}

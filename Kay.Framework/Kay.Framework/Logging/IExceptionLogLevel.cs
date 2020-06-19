using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kay.Framework.Logging
{
    public interface IExceptionLogLevel
    {
        LogLevel LogLevel { get; set; }
    }
}

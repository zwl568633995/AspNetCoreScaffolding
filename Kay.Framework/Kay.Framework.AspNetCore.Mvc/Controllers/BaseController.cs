using Kay.Framework.AspNetCore.Mvc.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kay.Framework.AspNetCore.Mvc.Controllers
{
    /// <summary>
    /// Controller基类封装
    /// </summary>
    [ApiWrappedFilter]
    public abstract class BaseController : ControllerBase
    {
        protected BaseController()
        {
        }

        #region Logger

        protected ILogger Logger => LazyLogger.Value;
        public ILoggerFactory LoggerFactory => HttpContext.RequestServices.GetRequiredService<ILoggerFactory>();
        private Lazy<ILogger> LazyLogger =>
            new Lazy<ILogger>(() => LoggerFactory?.CreateLogger(GetType().FullName) ?? NullLogger.Instance, true);

        #endregion
    }
}

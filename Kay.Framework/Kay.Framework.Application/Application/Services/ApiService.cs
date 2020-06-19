using Kay.Framework.Domain.UnitOfWork;
using Kay.Framework.ObjectMapping.Abstractions;
using Kay.Framework.RegisterInterfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kay.Framework.Application.Application.Services
{
    public class ApiService: IApiService
    {
        public IServiceProvider ServiceProvider { get; set; }

        protected ApiService(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        public IMapper Mapper => ServiceProvider.GetRequiredService<IMapper>();

        public IUnitOfWork UnitOfWork => ServiceProvider.GetRequiredService<IUnitOfWork>();

        #region Logger

        protected ILogger Logger => LazyLogger.Value;

        public ILoggerFactory LoggerFactory => ServiceProvider.GetRequiredService<ILoggerFactory>();

        private Lazy<ILogger> LazyLogger =>
            new Lazy<ILogger>(() => LoggerFactory?.CreateLogger(GetType().FullName) ?? NullLogger.Instance, true);

        #endregion
    }
}

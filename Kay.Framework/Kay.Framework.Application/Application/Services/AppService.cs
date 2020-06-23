using Kay.Framework.RegisterInterfaces;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.Extensions.DependencyInjection;
using Kay.Framework.Domain.UnitOfWork;
using Microsoft.Extensions.Logging.Abstractions;
using Kay.Framework.ObjectMapping.Abstractions;

namespace Kay.Framework.Application.Application.Services
{
    public class AppService:IAppService
    {
        public IServiceProvider ServiceProvider { get; set; }

        protected AppService(IServiceProvider serviceProvider)
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

using Kay.Framework.Domain.EntityFrameworkCore.Repositories;
using Kay.Framework.Domain.EntityFrameworkCore.UnitOfWork;
using Kay.Framework.Domain.Repositories;
using Kay.Framework.Domain.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kay.Framework.Domain.EntityFrameworkCore.DependencyInjection
{
    public static class AspNetExtensions
    {
        /// <summary>
        /// AddScoped注入DbContext
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDbContext<TDbContext>(this IServiceCollection services)
            where TDbContext : DbContext
        {
            services.AddScoped<DbContext, TDbContext>();
            return services;
        }

        /// <summary>
        /// AddScoped注入EfUnitOfWork
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddEfUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, EfUnitOfWork>();
            return services;
        }

        /// <summary>
        /// AddScoped 泛型注入IRepository
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddEfRepository(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<,>), typeof(EfRepository<,>));
            return services;
        }
    }
}

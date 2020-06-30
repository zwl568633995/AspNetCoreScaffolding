using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kay.Boilerplate.Infrastructure.BoundedContext.Ef;
using Kay.Framework.AspNetCore.Mvc.Attributes;
using Kay.Framework.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Kay.Framework.Extensions;
using Kay.Framework.AspNetCore.Mvc.EntityFrameworkCore;
using Kay.Framework.Domain.EntityFrameworkCore.DependencyInjection;
using Kay.Boilerplate.ApplicationService.IAppService;
using Kay.Boilerplate.Domain.Services;
using Kay.Framework.AspNetCore.Mvc.Containers;
using Kay.Framework.ObjectMapping.Abstractions;
using Kay.Framework.ObjectMapping.TinyMapper;
using Kay.Framework.AspNetCore.Exceptions;
using NLog.Web;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Kay.Framework.Redis;
using Kay.Boilerplate.Application.Service.Http.Filter;
using Kay.Framework.Swagger;

namespace Kay.Boilerplate.Application.Service.Http
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSession();

            #region 验证
            services.AddMvc(options =>
            {
                options.Filters.Add<ValidateModelAttribute>();
                options.Filters.Add<AuthorizationFilter>();
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            .AddJsonOptions(options =>
            {
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            });

            //关闭默认自动ValidateModel 验证
            services.Configure<ApiBehaviorOptions>(opts => opts.SuppressModelStateInvalidFilter = true);

            #endregion 验证

            #region Ef实现注入
            var dbType = Configuration.GetStringValue("DbType", "SqlServer");
            var dbConnection = Configuration.GetStringValue("DbConnectionString");

            services
                .AddDbContext<BoilerplateDbContext>(opt =>
                {
                    opt.UseNalongBuilder(dbType, dbConnection);
                })
                .AddDbContext<BoilerplateDbContext>()
                .AddEfUnitOfWork()
                .AddEfRepository();

            //Mysql的注入
            //services.AddDbContext<WebBoilerplateMysqlDbContext>(opt =>
            //{
            //    opt.UseMySql(Configuration.GetStringValue("nalong.mysql"));
            //});

            #endregion Ef实现注入

            #region AppService、DomainService、Config、AutoMapper 注入
           
            services.AddAppService(typeof(IUserAppService).Assembly);
            services.AddDomainService(typeof(TbUserDomainService).Assembly);
            services.AddSingleton(typeof(IMapper), typeof(TinyMapperMapper));

            #endregion AppService、DomainService、Config、AutoMapper 注入;

            #region Redis注入
            //redis连接字符串
            var redisConn = Configuration.GetSection("Redis").GetStringValue("ConnStr");
            services.AddSingleton(new RedisCliHelper(redisConn));
            #endregion

            #region Swagger注入
            services.AddSwaggerCustom(Configuration);
            #endregion
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            //添加异常中间件
            app.UseException();
            //app.UseResponseCompression();

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            #region Swagger
            app.UseSwaggerCustom(Configuration);
            #endregion

            #region 日志
            //引入NLog 日志组件注入
            loggerFactory.AddNLog();
            env.ConfigureNLog("NLog.config");
            #endregion

            //app.UseSession();
            app.UseMvc();

        }
    }
}

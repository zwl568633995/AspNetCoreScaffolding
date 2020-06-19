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
using NLog.Extensions.Configuration;
using Kay.Framework.Extensions;
using Kay.Framework.AspNetCore.Mvc.EntityFrameworkCore;
using Kay.Framework.Domain.EntityFrameworkCore.DependencyInjection;

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
            #region 日志
            var topic = Configuration.GetValue<string>("topic", "topic");
            services.AddNLog(Configuration, topic);
            #endregion

            #region NL-验证
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
            var dbType = Configuration.GetStringValue("dbType", "SqlServer");
            var dbConnection = Configuration.GetStringValue("dbConnectionString");

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

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc();
        }
    }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.IO;


namespace Kay.Framework.Swagger
{
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddSwaggerCustom(this IServiceCollection services, IConfiguration configuration)
        {
            //注册SwaggerAPI文档服务
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = configuration["GlobalSettings:ProjectName"],
                    Version = "v1",
                });


                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "添加token进行鉴权",
                    Name = "token",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                 {
                   {
                        new OpenApiSecurityScheme
                        {
                       Reference = new OpenApiReference {
                         Type = ReferenceType.SecurityScheme,
                         Id = "Bearer"
                     }
                     },
            new string[] { }
                  }
                  });

                //options.AddSecurityRequirement(new OpenApiSecurityRequirement());

                //获取应用程序根目录路径，官方写法
                var basePath = System.AppDomain.CurrentDomain.BaseDirectory;
                //linux环境下获取路径没有问题
                //var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
                //使用更简洁的AppContext.BaseDirectory、linux下也没问题
                //var basePath = AppContext.BaseDirectory;
                //设置Swagger注释  需要 右键项目 -> 生成  -> 输出 -> 勾选XML文档文件 才会产生XML文件
                var xmlPath = Path.Combine(basePath, "Kay.Boilerplate.Application.Service.Http.xml");
                if (System.IO.File.Exists(xmlPath))
                    options.IncludeXmlComments(xmlPath);
            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerCustom(this IApplicationBuilder builder, IConfiguration configuration)
        {
            //启用Swagger
            builder.UseSwagger();
            //启用SwaggerUI
            builder.UseSwaggerUI(options =>
            {
                //文档终结点
                options.SwaggerEndpoint("/swagger/v1/swagger.json", $"{configuration["GlobalSettings:ProjectName"]} API V1");
                //文档标题
                options.DocumentTitle = configuration["GlobalSettings:ProjectName"];
                //页面API文档格式 Full=全部展开， List=只展开列表, None=都不展开
                options.DocExpansion(DocExpansion.List);
            });
            return builder;
        }
    }
}

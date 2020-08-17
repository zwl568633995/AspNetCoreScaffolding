# AspNetCoreScaffolding
本框架在.netCore和.netStandard的基础上，集成了多种中间件

<h1 align="center"> .NetCore集成框架，即开即用</h1>


### 如果对您有帮助，您可以点右上角 "Star" 支持一下 谢谢！
### 如果您想获悉项目实时更新信息，您可以点右上角 "Fork" 感谢您的支持！


### 项目介绍
本框架是在.NetCore和.NetStandard的基础上，重写了一些基础组件，集成EF Core的核心功能，简化了传统手动抒写重复性代码的工作。
同时，框架集成了其他分布式组件和配置，节省了大量重复性的劳动，降低了开发成本，提高了整体开发效率，整体开发效率提高80%以上，欢迎大家使用及进行二次开发。

* 中间件和配置化：全新的架构和模块化的开发机制，便于灵活扩展和二次开发。
* EF Core:Add-migration和Update-database CodeFirst
* 支持SQLServer、MySQL、Oracle等多数据库类型，利用EF一键迁移，十分方便
* Token权限认证，常用的Redis缓存Token,也可集成IdentityServer4进行鉴权配置
* Swagger集成，接口管理更加方便
* 日志管理，NLog
* 集成Apollo，分布式配置
* 集成消息队列，Event消息
* Timer定时任务基础组件
* Docker一键部署，发布更加方便跨平台
* 适用范围：可以开发OA、ERP、BPM、CRM、WMS、TMS、MIS、BI、电商平台后台、物流管理系统、快递管理系统、教务管理系统等各类管理软件。
![image](https://github.com/zwl568633995/AspNetCoreScaffolding/blob/master/images/%E6%8A%80%E6%9C%AF%E9%80%89%E5%9E%8B.png)

## 开发者信息
* 系统名称：.NetCore集成框架  
* 作者：Kay (对，你看的没错，就是凯！！青龙志-凯)
* 微信：zwl568633995


## 技术支持

[技术支持微信：zwl568633995]


### .NetCore的中间件注入
```
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
            //    opt.UseMySql(Configuration.GetStringValue("mysql"));
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

            #region 定时任务注入
            services.AddHostedService<TestJobTrigger>();
            #endregion
```
以上是Core核心的注入功能，任何中间件和组件均可直接注入使用


## 更新说明

# 2020-06-22更新  
1、V1.0版本提交 至此系统可以使用了

# 2020-07-03更新  
1、新增定时任务，未用开源的Quartz和Hangfire,写了基础的采用System.Threading.Timer任务组件

## 安全&缺陷
如果你发现了一个安全漏洞或缺陷，请发送邮件到 568633995@qq.com,所有的安全漏洞都将及时得到解决。




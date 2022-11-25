using Autofac.Core;
using CrystalQuartz.AspNetCore;
using Hangfire;
using IdentityServer4;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Quartz;
using Quartz.Impl;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using Tiger.BackgroundWorker;
using Tiger.EntityFrameworkCore;
using Tiger.Infrastructure.BackgroundWorker;
using Tiger.MultiTenancy;
using Volo.Abp;
using Volo.Abp.Account.Web;
using Volo.Abp.AspNetCore.Authentication.JwtBearer;
using Volo.Abp.AspNetCore.ExceptionHandling;
using Volo.Abp.AspNetCore.MultiTenancy;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Basic;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Auditing;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs.Hangfire;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.BackgroundWorkers.Quartz;
using Volo.Abp.Data;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.Quartz;
using Volo.Abp.Timing;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.VirtualFileSystem;

namespace Tiger
{
    /// <summary>
    /// 包含应用程序的用户界面(UI).
    /// </summary>
    [DependsOn(
        typeof(TigerHttpApiModule),
        typeof(AbpAutofacModule),
        typeof(AbpAspNetCoreMultiTenancyModule),
        typeof(TigerApplicationModule),
        typeof(TigerEntityFrameworkCoreDbMigrationsModule),
        typeof(AbpAspNetCoreMvcUiBasicThemeModule),
        typeof(AbpAspNetCoreAuthenticationJwtBearerModule),
        typeof(AbpAccountWebIdentityServerModule),
        //typeof(AbpAspNetCoreSerilogModule),  // 设置依赖于 SerilogModule 日志组件
        typeof(AbpBackgroundJobsHangfireModule), //Hangfire 定时作业模块依赖
        //typeof(AbpBackgroundWorkersModule),  // ABP默认后台工作者
        typeof(AbpBackgroundWorkersQuartzModule) //Quartz 定时任务(abp叫后台工作者)
        )]
    public class TigerHttpApiHostModule : AbpModule
    {
        private const string DefaultCorsPolicyName = "Default";


        /// <summary>
        /// 预配置服务
        /// </summary>
        /// <param name="context"></param>
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {

            #region 配置Quartz后台工作者
            //定时任务。
            var configuration = context.Services.GetConfiguration();


            //PreConfigure<AbpQuartzOptions>(options =>
            //{
            //    options.Properties = new NameValueCollection
            //    {
            //        ["quartz.jobStore.dataSource"] = "BackgroundJobsDemoApp",
            //        ["quartz.jobStore.type"] = "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz",
            //        ["quartz.jobStore.tablePrefix"] = "QRTZ_",
            //        ["quartz.serializer.type"] = "json",
            //        ["quartz.dataSource.BackgroundJobsDemoApp.connectionString"] = configuration.GetConnectionString("Quartz"),
            //        ["quartz.dataSource.BackgroundJobsDemoApp.provider"] = "SqlServer",
            //        ["quartz.jobStore.driverDelegateType"] = "Quartz.Impl.AdoJobStore.SqlServerDelegate, Quartz",
            //    };
            //});


            //从ABP3.1版本开始,我们在 AbpQuartzOptions 添加了 Configurator 用于配置Quartz. 例:
            PreConfigure<AbpQuartzOptions>(options =>
            {
                options.Configurator = configure =>
                {
                    configure.UsePersistentStore(storeOptions =>
                    {
                        // // serialization format breaks, defaults to false
                        storeOptions.UseProperties = false;
                        // this requires Quartz.Serialization.Json NuGet package
                        storeOptions.UseJsonSerializer();
                        // 将作业与调度信息存储数据中
                        // 如果没有表需要手动创建 根据官网文档资料： https://github.com/quartznet/quartznet/tree/main/database/tables
                        storeOptions.UseSqlServer(configuration.GetConnectionString("Quartz"));
                        storeOptions.UseClustering(c =>
                        {
                            c.CheckinMisfireThreshold = TimeSpan.FromSeconds(20);
                            c.CheckinInterval = TimeSpan.FromSeconds(10);
                        });
                    });
                };
            });

            

            #endregion
        }

        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            var hostingEnvironment = context.Services.GetHostingEnvironment();

            

            ConfigureUrls(configuration);
            ConfigureConventionalControllers();
            ConfigureAuthentication(context, configuration);
            ConfigureLocalization();
            ConfigureVirtualFileSystem(context);
            ConfigureAuditing(context);
            ConfigureCors(context, configuration);
            ConfigureSwaggerServices(context);
            ConfigureHangfire(context, configuration);
            ConfigureTiming(context, configuration);

            #region 配置Abp  请求异常信息
            // Abp项目默认会启动内置的异常处理，默认不将异常信息发送到客户端。
            Configure<AbpExceptionHandlingOptions>(options =>
            {
#if DEBUG
                options.SendExceptionsDetailsToClients = true;
#else
                options.SendExceptionsDetailsToClients = false;
#endif
            });
            #endregion

            #region 配置Quartz后台工作者
            //Configure<AbpBackgroundWorkerQuartzOptions>(options =>
            //{
            //    // 全局自动添加
            //    options.IsAutoRegisterEnabled = false;
                
            //});



            #endregion

            #region 配置CrystalQuartz管理面板
            //配置可以同步请求读取流数据
            // 不配置开启同步流读取 CrystalQuartz 管理面板无法打开。。 
            Configure<KestrelServerOptions>(x => x.AllowSynchronousIO = true);
            Configure<IISServerOptions>(x => x.AllowSynchronousIO = true);
            #endregion

        }

        #region 配置Urls
        private void ConfigureUrls(IConfiguration configuration)
        {

            Configure<AppUrlOptions>(options =>
            {
                options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
            });
        } 
        #endregion

        #region 配置虚拟文件系统
        /// <summary>
        /// 配置虚拟文件系统
        /// </summary>
        /// <param name="context"></param>
        private void ConfigureVirtualFileSystem(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();

            if (hostingEnvironment.IsDevelopment())
            {
                Configure<AbpVirtualFileSystemOptions>(options =>
                {
                    options.FileSets.ReplaceEmbeddedByPhysical<TigerDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Tiger.Domain.Shared"));
                    options.FileSets.ReplaceEmbeddedByPhysical<TigerDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Tiger.Domain"));
                    options.FileSets.ReplaceEmbeddedByPhysical<TigerApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Tiger.Application.Contracts"));
                    options.FileSets.ReplaceEmbeddedByPhysical<TigerApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, $"..{Path.DirectorySeparatorChar}Tiger.Application"));
                });
            }
        }
        #endregion

        #region ConfigureConventionalControllers
        private void ConfigureConventionalControllers()
        {
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(TigerApplicationModule).Assembly);
            });
        } 
        #endregion

        #region 配置identityServer授权认证服务
        /// <summary>
        /// 配置identityServer授权认证服务
        /// </summary>
        /// <param name="context"></param>
        /// <param name="configuration"></param>
        private void ConfigureAuthentication(ServiceConfigurationContext context, IConfiguration configuration)
        {

            //var builder = context.Services.AddIdentityServer()
            //   .AddInMemoryIdentityResources(Config.IdentityResources)
            //   //.AddInMemoryApiScopes(Config.ApiScopes)
            //   .AddInMemoryClients(Config.Clients)
            //   .AddTestUsers(TestUsers.Users);



            context.Services.AddAuthentication(options =>
                {
                    //options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    //options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                })
                .AddJwtBearer(options =>
                {
                    options.Authority = configuration["AuthServer:Authority"];
                    options.RequireHttpsMetadata = false;
                    options.Audience = "Tiger";

                    options.BackchannelHttpHandler = new HttpClientHandler()
                    {
                        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                    };
                })

            // 自己添加的配置
                .AddOpenIdConnect("oidc", "Tiger IdentityServer", options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                    options.SignOutScheme = IdentityServerConstants.SignoutScheme;
                    options.SaveTokens = true;

                    // 线上没有https 开发模式需要设置false 不然无法运行
                    options.RequireHttpsMetadata = false;

                    options.Authority = configuration["AuthServer:Authority"];
                    options.ClientId = "interactive.confidential";
                    options.ClientSecret = "secret";
                    options.ResponseType = "code";

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = "name",
                        RoleClaimType = "role"
                    };
                });
            //.AddGitHub(options =>
            //{
            //    options.ClientId = configuration["Github:ClientID"];
            //    options.ClientSecret = configuration["Github:ClientSecret"];
            //});
        }
        #endregion

        #region 配置SwaggerUI
        /// <summary>
        /// 配置SwaggerUI
        /// </summary>
        /// <param name="context"></param>
        private static void ConfigureSwaggerServices(ServiceConfigurationContext context)
        {
            context.Services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("api", new OpenApiInfo
                    {
                        Title = "Tiger API接口",
                        Version = "v1",
                        Description = "Tiger API 接口说明文档",
                        //TermsOfService = "None",
                        TermsOfService = new Uri("http://www.baidu.com"),
                        Contact = new OpenApiContact
                        {
                            Name = "hongjy",
                            Email = "hongjy1991@gmail.com",
                        },
                        License = new OpenApiLicense
                        {
                            Name = "MIT",
                        }
                    }); ;

                    options.SwaggerDoc("admin", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "Tiger接口",
                        Description = "Tiger接口",

                        //API 服务条款的 URL。 必须采用 URL 格式。
                        TermsOfService = new Uri("https://example.com/terms"),
                        Contact = new OpenApiContact
                        {
                            Name = "hongjy",
                            Email = "hongjy1991@gmail.com",
                            Url = new Uri("https://www.hongjy.cn/"),
                        },
                        License = new OpenApiLicense
                        {
                            Name = "Use under LICX",
                        }
                    });

                    options.SwaggerDoc("admin-basic", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "Tiger Basic接口",
                        Description = "Tiger 后台 Basic接口",
                    });

                    options.SwaggerDoc("admin-erp", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "进销存",
                        Description = "采购库存相关接口",
                    });



                    //根据分组来设置要展示的接口
                    options.DocInclusionPredicate((docName, apiDes) =>
                    {
                        if (!apiDes.TryGetMethodInfo(out MethodInfo method))
                            return false;
                        /*使用ApiExplorerSettingsAttribute里面的GroupName进行特性标识
                         * DeclaringType只能获取controller上的特性
                         * 我们这里是想以action的特性为主
                         * */
                        var version = method.DeclaringType.GetCustomAttributes(true).OfType<ApiExplorerSettingsAttribute>().Select(m => m.GroupName);
                        if (docName == "admin" && !version.Any())
                            return true;
                        //这里获取action的特性
                        var actionVersion = method.GetCustomAttributes(true).OfType<ApiExplorerSettingsAttribute>().Select(m => m.GroupName);
                        if (actionVersion.Any())
                            return actionVersion.Any(v => v == docName);
                        return version.Any(v => v == docName);
                    });
                    //options.DocInclusionPredicate((docName, description) => true);


                    //添加授权 登录的小绿锁
                    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Description = "请输入带有Bearer开头的Token",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey
                    });
                    ////认证方式，此方式为全局添加
                    //options.AddSecurityRequirement(new OpenApiSecurityRequirement<string, IEnumerable<string>>
                    //{
                    //    { "Bearer", Enumerable.Empty<string>() }
                    // });


                    // 为 Swagger JSON and UI设置xml文档注释路径
                    //获取应用程序所在目录（绝对，不受工作目录影响，建议采用此方法获取路径）
                    var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);

                    var xmlPathHost = Path.Combine(basePath, "Tiger.HttpApi.Host.xml");
                    var xmlPathHttpApi = Path.Combine(basePath, "Tiger.HttpApi.xml");
                    var xmlPathApplication = Path.Combine(basePath, "Tiger.Application.xml");
                    var xmlPathContracts = Path.Combine(basePath, "Tiger.Application.Contracts.xml");


                    options.IncludeXmlComments(xmlPathHost);
                    options.IncludeXmlComments(xmlPathHttpApi);
                    options.IncludeXmlComments(xmlPathApplication);
                    options.IncludeXmlComments(xmlPathContracts);
                    options.EnableAnnotations();//注释



                });
        }
        #endregion

        #region 多语言配置
        /// <summary>
        /// 多语言配置
        /// </summary>
        private void ConfigureLocalization()
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                //options.Languages.Add(new LanguageInfo("ar", "ar", "العربية"));
                //options.Languages.Add(new LanguageInfo("cs", "cs", "Čeština"));
                //options.Languages.Add(new LanguageInfo("en", "en", "English"));
                //options.Languages.Add(new LanguageInfo("fr", "fr", "Français"));
                //options.Languages.Add(new LanguageInfo("pt-BR", "pt-BR", "Português"));
                //options.Languages.Add(new LanguageInfo("ru", "ru", "Русский"));
                //options.Languages.Add(new LanguageInfo("tr", "tr", "Türkçe"));
                options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
                //options.Languages.Add(new LanguageInfo("zh-Hant", "zh-Hant", "繁體中文"));
            });
        }
        #endregion

        #region 跨域配置
        /// <summary>
        /// 跨域配置
        /// </summary>
        /// <param name="context"></param>
        /// <param name="configuration"></param>
        private void ConfigureCors(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.Services.AddCors(options =>
            {
                options.AddPolicy(DefaultCorsPolicyName, builder =>
                {
                    builder
                        .WithOrigins(
                            configuration["App:CorsOrigins"]
                                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                .Select(o => o.RemovePostFix("/"))
                                .ToArray()
                        )
                        .WithAbpExposedHeaders()
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
        }
        #endregion

        #region 配置审计日志
        /// <summary>
        /// 配置审计日志
        /// </summary>
        private void ConfigureAuditing(ServiceConfigurationContext context)
        {
            Configure<AbpAuditingOptions>(options =>
            {
                options.IsEnabled = true; //Disables the auditing system  (默认值: true): 启用或禁用审计系统的总开关. 如果值为 false,则不使用其他选项.
                options.HideErrors = false; // (默认值: true) 在保存审计日志对象时如果发生任何错误,审计日志系统会将错误隐藏并写入常规日志. 如果保存审计日志对系统非常重要那么将其设置为 false 以便在隐藏错误时抛出异常.
                options.IsEnabledForAnonymousUsers = true; // 如果只想为经过身份验证的用户记录审计日志,请设置为 false.如果为匿名用户保存审计日志,你将看到这些用户的 UserId 值为 null.
#if DEBUG
                options.IsEnabledForGetRequests = false; // HTTP GET请求通常不应该在数据库进行任何更改,审计日志系统不会为GET请求保存审计日志对象. 将此值设置为 true 可为GET请求启用审计日志系统.

                //保存所有实体的所有变化 将占用大量的数据库空间看情况开启
                options.EntityHistorySelectors.AddAllEntities();
#endif
            });
        }
        #endregion

        #region 配置数据过滤
        /// <summary>
        /// 配置数据过滤
        /// </summary>
        /// <param name="context"></param>
        private void ConfigureDataFiltering(ServiceConfigurationContext context)
        {
            ////配置默认情况下禁用了 ISoftDelete 过滤,除非显示启用,在查询数据库时会包含标记为已删除的实体:
            //Configure<AbpDataFilterOptions>(options =>
            //{
            //    options.DefaultStates[typeof(ISoftDelete)] = new DataFilterState(isEnabled: false);
            //});

        }
        #endregion

        #region 配置后台作业集成Hangfire
        /// <summary>
        /// 配置后台作业集成Hangfire
        /// </summary>
        /// <param name="context"></param>
        /// <param name="configuration"></param>
        private void ConfigureHangfire(ServiceConfigurationContext context, IConfiguration configuration)
        {
            // 配置Hangfire存储和连接字符串
            context.Services.AddHangfire(config =>
            {
                config.UseSqlServerStorage(configuration.GetConnectionString("Hangfire"));
            });
        }
        #endregion

        #region 配置时区
        /// <summary>
        /// 配置时区
        /// </summary>
        /// <param name="context"></param>
        /// <param name="configuration"></param>
        private void ConfigureTiming(ServiceConfigurationContext context, IConfiguration configuration)
        {
            Configure<AbpClockOptions>(options =>
            {
                //AbpClockOptions 是用于设置时钟种类的选项类.
                options.Kind = DateTimeKind.Utc;
            });

        } 
        #endregion

        #region 应用初始化
        /// <summary>
        /// 应用初始化
        /// </summary>
        /// <param name="context"></param>
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAbpRequestLocalization();

            if (!env.IsDevelopment())
            {
                app.UseErrorPage();
            }

            app.UseCorrelationId();
            app.UseVirtualFiles();
            app.UseRouting();
            app.UseCors(DefaultCorsPolicyName);
            app.UseAuthentication();
            app.UseJwtTokenMiddleware();

            if (MultiTenancyConsts.IsEnabled)
            {
                app.UseMultiTenancy();
            }

            app.UseIdentityServer();
            app.UseAuthorization();

            #region swaggerui 配置
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger(options =>
            {
                // 改为openAPI 2.0版本 默认是3.0版本
                //options.SerializeAsV2 = true;
            });
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(options =>
            {
                // 配置自定义的样式
                //options.InjectStylesheet("/swagger-ui/custom.css");

                options.SwaggerEndpoint("/swagger/admin/swagger.json", "Admin-系统设置");
                options.SwaggerEndpoint("/swagger/admin-basic/swagger.json", "Admin-订单商品营销");
                options.SwaggerEndpoint("/swagger/admin-erp/swagger.json", "Admin-采购库存");
                options.SwaggerEndpoint("/swagger/api/swagger.json", "API-App接口");

                //options.SwaggerEndpoint("/swagger/auth/swagger.json", "Auth");
                //options.SwaggerEndpoint("/swagger/gp/swagger.json", "登录模块");
                //options.SwaggerEndpoint("/swagger/mom/swagger.json", "业务模块");
                //options.SwaggerEndpoint("/swagger/dm/swagger.json", "其他模块");

                // 设置接口文档默认不展开
                options.DocExpansion(DocExpansion.None);
                options.DefaultModelExpandDepth(1);

                // API前缀设置为空
                options.RoutePrefix = string.Empty;
                // API页面Title
                options.DocumentTitle = "Tiger接口文档 - 花生了什么树";


                options.OAuthClientId("testOauthClientId");
            });
            #endregion

            app.UseAuditing();
            //app.UseAbpSerilogEnrichers(); // 使用Serilog日志
            app.UseConfiguredEndpoints();

            #region 后台工作者 CrystalQuartz控制面板

            // 组件地址: https://github.com/guryanovev/CrystalQuartz

            //// TODO:修改为 从ABP的类获取这个对象
            //var scheduler = CreateScheduler();
            //app.UseCrystalQuartz(() => scheduler);
            #endregion

            #region 后台工作者
            // 在应用程序运行时候添加定时任务
            //context.AddBackgroundWorker<PassiveUserCheckerWorker>();
            #endregion

            #region 后台作业 Hangfire集成配置
            // TODO:封装方法 同意管理新增的后台作业
            // 使用Hangfire的面板
            // Hangfire默认管理地址： https://localhost:44306/hangfire/
            app.UseHangfireDashboard();

            // 新建一个项目单独运行定时任务
            // 确保你的web应用程序被配置为始终运行. 否则只有在你的应用程序正在运行时后台作业才会工作.
            // Todo:后台工作者 集成 haigfire

             
            #endregion



        }


        private static IScheduler CreateScheduler()
        {
            var schedulerFactory = new StdSchedulerFactory();
            var scheduler = schedulerFactory.GetScheduler().Result;

            var job = JobBuilder.Create<MyQuartzLogWorker>()
                .WithIdentity("localJob", "default")
                .Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "default")
                .ForJob(job)
                .StartNow()
                .WithCronSchedule("0 /1 * ? * *")
                .Build();

            scheduler.ScheduleJob(job, trigger);

            scheduler.Start();

            return scheduler;
        }






        #endregion
    }
}

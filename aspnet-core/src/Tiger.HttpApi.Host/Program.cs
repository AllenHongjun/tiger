using System;
using log4net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Tiger.Extensions;

namespace Tiger
{
    public class Program
    {
        public static int Main(string[] args)
        {



            string logFileName = DateTime.Now.ToString("yyyy-MM-dd");
            logFileName = "Logs/" + logFileName + "-logs.txt";
            Log.Logger = new LoggerConfiguration()
#if DEBUG
                //.MinimumLevel.Debug()
                //.MinimumLevel.Error()
#else
                .MinimumLevel.Information()
#endif

                .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
                .Enrich.FromLogContext()
                .WriteTo.Async(c => c.File(logFileName))
                //#if DEBUG
                //                .WriteTo.Async(c => c.Console())
                //#endif
                .CreateLogger();

            try
            {
                Log.Information("Starting Tiger.HttpApi.Host.");
                CreateHostBuilder(args).Build().Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly!");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        internal static IHostBuilder CreateHostBuilder(string[] args) =>

                // .NET Core 和 ASP.NET Core 中的日志记录 https://learn.microsoft.com/zh-cn/aspnet/core/fundamentals/logging/?view=aspnetcore-3.1#logging-providers
                // 调用 CreateDefaultBuilder 将添加以下日志记录提供程序 控制台 调试 EventSource Eventlog
                Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    //// 若要替代Host.CreateDefaultBuilder 添加的默认日志记录提供程序集，请调用 ClearProviders 并添加所需的日志记录提供程序。
                    logging.ClearProviders();
                    logging.AddConsole();
                    logging.AddDebug();
                    logging.AddEventLog();
                    logging.AddEventSourceLogger();
                })
                // 配置使用 Log4Net 等其他第三方的日志中提供程序
                //.ConfigureLogging((context, loggingBuilder) =>
                //{
                //    loggingBuilder.AddFilter("System", LogLevel.Information);
                //    loggingBuilder.AddFilter("Microsoft", LogLevel.Information);
                //    var path = context.HostingEnvironment.ContentRootPath;
                //    loggingBuilder.AddLog4Net($"{path}/.config/log4net.config");//配置文件

                //})
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })

                .UseAutofac() //Integrate Autofac!
                .UseSerilog();


    }
}

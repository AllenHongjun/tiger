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
            CreateHostBuilder(args).Build().Run();
            return 0;
        }


        /// <summary>
        /// Serilog日志模板
        /// </summary>
        static string serilogDebug = System.Environment.CurrentDirectory + @"\Logs\Debug\.log";
        static string serilogInfo = System.Environment.CurrentDirectory + @"\Logs\Info\.log";
        static string serilogWarn = System.Environment.CurrentDirectory + @"\Logs\\Warning\.log";
        static string serilogError = System.Environment.CurrentDirectory + @"\Logs\Error\.log";
        static string serilogFatal = System.Environment.CurrentDirectory + @"\Logs\Fatal\.log";

        static string SerilogOutputTemplate = "{NewLine}时间:{Timestamp:yyyy-MM-dd HH:mm:ss.fff}{NewLine}日志等级:{Level}{NewLine}所在类:{SourceContext}{NewLine}日志信息:{Message}{NewLine}{Exception}";




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
                .UseSerilog((context, logger) => //注册Serilog
                {
                    // Serilog 使用和配置 https://blog.csdn.net/e_hilary/article/details/118925669
                    //// 从配置文件中读取
                    //logger.ReadFrom.Configuration(context.Configuration);
                    //logger.Enrich.FromLogContext();


                    // TODO: 配合配置文件 从配置文件从读取日志配置
                    logger.WriteTo.Console();  // 输出到Console控制台
                                               // 输出到配置的文件日志目录
                    logger.WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Debug)
                    .WriteTo.Async(a => a.File(serilogDebug, rollingInterval: RollingInterval.Hour, outputTemplate: SerilogOutputTemplate)))
                        
                    .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Information)
                    .WriteTo.Async(a => a.File(serilogInfo, rollingInterval: RollingInterval.Hour, outputTemplate: SerilogOutputTemplate)))
                        
                    .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Warning)
                    .WriteTo.Async(a => a.File(serilogWarn, rollingInterval: RollingInterval.Hour, outputTemplate: SerilogOutputTemplate)))
                        
                    .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Error)
                    .WriteTo.Async(a => a.File(serilogError, rollingInterval: RollingInterval.Hour, outputTemplate: SerilogOutputTemplate)))
                        
                    .WriteTo.Logger(lg => lg.Filter.ByIncludingOnly(p => p.Level == LogEventLevel.Fatal)
                    .WriteTo.Async(a => a.File(serilogFatal, rollingInterval: RollingInterval.Hour, outputTemplate: SerilogOutputTemplate)));


                }); // 使用Serilog


    }
}

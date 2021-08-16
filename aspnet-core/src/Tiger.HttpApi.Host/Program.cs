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

            //var Logger = LogManager.GetLogger(typeof(Program));


            //try
            //{
            //    Logger.Info("Starting Tiger.HttpApi.Host.");
            //    CreateHostBuilder(args).Build().Run();
            //    return 0;
            //}
            //catch (Exception ex)
            //{
            //    Logger.Error("Host terminated unexpectedly!", ex);
            //    return 1;
            //}
            //finally
            //{
            //    Logger.Error("程序结束运行");
            //}




            Log.Logger = new LoggerConfiguration()
#if DEBUG
                .MinimumLevel.Debug()
#else
                .MinimumLevel.Information()
#endif
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Async(c => c.File("Logs/logs.txt"))
#if DEBUG
                .WriteTo.Async(c => c.Console())
#endif
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
                Host.CreateDefaultBuilder(args)
                .ConfigureLogging((context, loggingBuilder) =>
                {
                    loggingBuilder.AddFilter("System", LogLevel.Information);
                    loggingBuilder.AddFilter("Microsoft", LogLevel.Information);
                    var path = context.HostingEnvironment.ContentRootPath;
                    loggingBuilder.AddLog4Net($"{path}/.config/log4net.config");//配置文件
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                
                .UseAutofac()
                .UseSerilog();

             


    }
}

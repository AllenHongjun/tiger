using System;
using System.IO;
using log4net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Tiger.Extensions;

namespace Tiger
{
    public class Program
    {

        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        public static int Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                 #region Serilog日志代码配置
                //.Enrich.FromLogContext()
                //.MinimumLevel.Debug()
                //.WriteTo.Debug()
                //.WriteTo.Console(
                //    outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}",
                //    restrictedToMinimumLevel: LogEventLevel.Debug)
                //.WriteTo.File("Logs/Warning/log.txt")

                //// 配置不同的错误写入不同的日志文件
                //.WriteTo.File("Logs/Warning/log.txt",
                //        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                //        rollingInterval: RollingInterval.Day,
                //        restrictedToMinimumLevel: LogEventLevel.Warning)
                //.WriteTo.File("Logs/Error/log.txt",
                //        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                //        rollingInterval: RollingInterval.Day,
                //        restrictedToMinimumLevel: LogEventLevel.Error)
                //.WriteTo.File("Logs/Fatal/log.txt",
                //        outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                //        rollingInterval: RollingInterval.Day,
                //        restrictedToMinimumLevel: LogEventLevel.Fatal) 
                 #endregion
                .CreateLogger();

            try
            {   
                
                Log.Information("Starting web host");
                CreateHostBuilder(args).Build().Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }




        internal static IHostBuilder CreateHostBuilder(string[] args) =>
                Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog()
                .UseAutofac(); //Integrate Autofac!



    }
}

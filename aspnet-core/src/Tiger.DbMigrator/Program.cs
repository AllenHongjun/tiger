using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace Tiger.DbMigrator
{   
    /// <summary>
    /// 有这样一个单独的控制台应用程序有几个优点;
    /// 
    /// 部署到新的服务器 或者 数据库字段有修改是 有迁移内容 优先更新的程序
    ///你可以在更新你的应用程序之前运行它,所以你的应用程序可以在准备就绪的数据库上运行.
    ///与本身初始化种子数据相比,你的应用程序启动速度更快.
    ///应用程序可以在集群环境中正确运行(其中应用程序的多个实例并发运行). 在这种情况下如果在应用程序启动时播种数据就会有冲突.
    /// </summary>
    class Program
    {
        static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Volo.Abp", LogEventLevel.Warning)
#if DEBUG
                .MinimumLevel.Override("Tiger", LogEventLevel.Debug)
#else
                .MinimumLevel.Override("Tiger", LogEventLevel.Information)
#endif
                .Enrich.FromLogContext()
                .WriteTo.Async(c => c.File("Logs/logs.txt"))
                .WriteTo.Async(c => c.Console())
                .CreateLogger();

            await CreateHostBuilder(args).RunConsoleAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging((context, logging) => logging.ClearProviders())
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<DbMigratorHostedService>();
                });
    }
}

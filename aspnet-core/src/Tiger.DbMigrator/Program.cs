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
    /// ������һ�������Ŀ���̨Ӧ�ó����м����ŵ�;
    /// 
    /// �����µķ����� ���� ���ݿ��ֶ����޸��� ��Ǩ������ ���ȸ��µĳ���
    ///������ڸ������Ӧ�ó���֮ǰ������,�������Ӧ�ó��������׼�����������ݿ�������.
    ///�뱾���ʼ�������������,���Ӧ�ó��������ٶȸ���.
    ///Ӧ�ó�������ڼ�Ⱥ��������ȷ����(����Ӧ�ó���Ķ��ʵ����������). ����������������Ӧ�ó�������ʱ�������ݾͻ��г�ͻ.
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

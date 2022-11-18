//using log4net;
//using log4net.Config;
//using log4net.Repository;
using Hangfire;
using Hangfire.SqlServer;
using System;
using System.IO;
//using Volo.Abp.BackgroundJobs;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            //ILoggerRepository repository = LogManager.CreateRepository("NETCoreRepository");
            //// 默认简单配置，输出至控制台
            //BasicConfigurator.Configure(repository);
            //ILog log = LogManager.GetLogger(repository.Name, "NETCorelog4net");

            //log.Info("NETCorelog4net log");
            //log.Info("test log");
            //log.Error("error");
            //log.Info("linezero");
            //Console.ReadKey();


            //ILoggerRepository repository = LogManager.CreateRepository("NETCoreRepository");
            //XmlConfigurator.Configure(repository, new FileInfo("log4net.config"));
            //ILog log = LogManager.GetLogger(repository.Name, "NETCorelog4net");

            //log.Info("NETCorelog4net log");
            //log.Info("test log");
            //log.Error("error");
            //log.Info("linezero");



            GlobalConfiguration.Configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseColouredConsoleLogProvider()
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage("Database=Hangfire.Sample; Integrated Security=True;", new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true
                });

            BackgroundJob.Enqueue(() => Console.WriteLine("Hello, world!"));

            using (var server = new BackgroundJobServer())
            {
                Console.ReadLine();
            }









            Console.ReadKey();
        }
    }
}

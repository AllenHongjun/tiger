using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tiger.PlatfromSdk.DogApi.Clients.Interfaces;
using Volo.Abp.BackgroundWorkers.Quartz;
using Volo.Abp.Identity;

namespace Tiger.Infrastructure.BackgroundWorker.DogApi
{
    public class ListAllBreedsWorker : QuartzBackgroundWorkerBase
    {
        public ListAllBreedsWorker()
        {

             JobDetail = JobBuilder.Create<ListAllBreedsWorker>()
                .WithIdentity("Dog Api", "Api")
                .Build();

             Trigger = TriggerBuilder.Create()
                .WithIdentity("ms_trigger", "Api")
                .ForJob(JobDetail)
                .StartNow()
                .WithCronSchedule("0 0/10 * ? * *")
                .Build();


            ScheduleJob = async scheduler =>
            {
                if (!await scheduler.CheckExists(JobDetail.Key))
                {
                    await scheduler.ScheduleJob(JobDetail, Trigger);
                }
            };
        }

        public async override Task Execute(IJobExecutionContext context)
        
        {
            //var client = new RestClient("https://dog.ceo/api/breeds/list/all");
            //var request = new RestRequest(new Uri("https://dog.ceo/api/breeds/list/all"), Method.Get);
            //var response = client.Execute(request);
            //Console.WriteLine($"ListAllBreeds response {response.Content}{DateTime.Now.ToString()}");
            Logger.LogInformation($"Executed ListAllBreedsWorker!  执行时间：{DateTime.Now.ToString()}");

            var dogClient = ServiceProvider.GetService<IDogClient>();
            var response =  await dogClient.GetAllBreeds();
            
            Logger.LogInformation($"Executed ListAllBreedsWorker! {response.message}  执行时间：{DateTime.Now.ToString()}",response.message);
            return;

            //var options = new RestClientOptions("https://dog.ceo/api")
            //{
            //    ThrowOnAnyError = true,
            //    MaxTimeout = 1000
            //};
            //var client = new RestClient(options);

            // 请求接口 

            // 下载文件

            // 上传文件服务器

            // 

            // 数据入库

            //TestRestClient();
            
        }


        private async void TestRestClient()
        {

            // 实例化客户端
            //var client = new RestClient("https://dog.ceo/api");

            var options = new RestClientOptions("https://dog.ceo")
            {
                ThrowOnAnyError = true,
                MaxTimeout = 1000
            };
            var client = new RestClient(options);

            // 创建请求
            var request = new RestRequest("sbreeds/list/all").AddQueryParameter("foo", "bar")
                    .AddJsonBody(new { test = 1 });

            //var response = await client.PostAsync<MyResponse>(request, cancellationToken);
            //var response = await client.PostAsync(request);
            var response = await client.GetAsync(request);
            Logger.LogInformation($"ListAllBreeds response {response.Content}{DateTime.Now.ToString()}");
        }
    }
}

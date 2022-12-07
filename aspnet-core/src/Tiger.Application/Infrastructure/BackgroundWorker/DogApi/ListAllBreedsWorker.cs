using Quartz;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.BackgroundWorkers.Quartz;

namespace Tiger.Infrastructure.BackgroundWorker.DogApi
{
    public class ListAllBreedsWorker : QuartzBackgroundWorkerBase
    {

        public async override Task Execute(IJobExecutionContext context)
        {
            //var client = new RestClient("https://dog.ceo/api/breeds/list/all");
            //var request = new RestRequest(new Uri("https://dog.ceo/api/breeds/list/all"), Method.Get);
            //var response = client.Execute(request);
            //Console.WriteLine($"ListAllBreeds response {response.Content}{DateTime.Now.ToString()}");
            //Logger.LogInformation($"Executed CrystalQuartzLogWorker..!  执行时间：{DateTime.Now.ToString()}");


            var options = new RestClientOptions("https://dog.ceo/api")
            {
                ThrowOnAnyError = true,
                MaxTimeout = 1000
            };
            var client = new RestClient(options);

            var request = new RestRequest("sbreeds/list/all").AddQueryParameter("foo", "bar")
                    .AddJsonBody(new { test = 1 });

            var response = await client.GetAsync(request);

            return;
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
            Console.WriteLine($"ListAllBreeds response {response.Content}{DateTime.Now.ToString()}");
        }
    }
}

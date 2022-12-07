using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serializers;
using SpotifyAPI.Web;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using static SpotifyAPI.Web.SearchRequest;

namespace Tiger.Infrastructure.Rest
{
    [ApiExplorerSettings(GroupName = "admin")]
    [RemoteService(false)]
    public class RestService : TigerAppService
    {

        private readonly ILogger _logger;

        public RestService(ILogger<RestService> logger)
        {
            _logger=logger;
        }

        public async void TestRequest()
        {
            var client = new RestClient("https://api.twitter.com/1.1")
            {
                Authenticator = new HttpBasicAuthenticator("username", "password")
            };
            var request = new RestRequest("statuses/home_timeline.json");
            var response = await client.GetAsync(request);

            _logger.LogInformation("twitter Authenticator", response);
        }

        public async void TestBasicUse()
        {
            var client = new RestClient("https://api.twitter.com/1.1");
            client.Authenticator = new HttpBasicAuthenticator("username", "password");

            var request = new RestRequest("statuses/home_timeline.json");
            var params1 = new
            {
                status = 1,
                priority = "high",
                ids = new[] { "123", "456" }
            };
            request.AddObject (params1);

            const string json = "{ data: { foo: \"bar\" } }";
            request.AddStringBody(json, ContentType.Json);


            var param2 = new  { IntData = 1, StringData = "test123" };
            request.AddJsonBody(param2);

            //// Adds a file from disk
            //request.AddFile(parameterName, filePath, contentType);

            //// Adds an array of bytes
            //request.AddFile(parameterName, bytes, fileName, contentType);

            //// Adds a stream returned by the getFile function
            //request.AddFile(parameterName, getFile, fileName, contentType);

            await client.DownloadDataAsync(request);

            await client.DownloadStreamAsync(request);

            var request2 = new RestRequest("statuses/home_timeline.json", Method.Post);

            var request3 = new RestRequest("health/{entity}/status")
                .AddUrlSegment("entity", "s2");

            //var timeline = await client.GetAsync<HomeTimeline>(request, cancellationToken);
        }


        




        /// <summary>
        /// 请求接口测试
        /// </summary>
        public async void TestOauthToken()
        {

        }


    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace Tiger.Infrastructure.Rest
{
    [ApiExplorerSettings(GroupName = "admin")]
    [RemoteService(true)]
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


        /// <summary>
        /// 请求接口测试
        /// </summary>
        public async void TestOauthToken()
        {

            var client = new RestClient("https://sso.auth.wayfair.com/oauth/token");
            var request = new RestRequest("https://sso.auth.wayfair.com/oauth/token", Method.Post);
            request.AddHeader("Authorization", "Bearer eyJhbGciOiJSUzI1NiIsImtpZCI6InBlRG5BMWVVVGRVQU00YVdjU3FnZm40ZEJhbFZCYnJ4R2ZEU0ZQYXVQbG8iLCJ0eXAiOiJKV1QiLCJ4NXQiOiJtUkJLODdWREcwWWJCYXVOS2ZkbnE0ejNZUW8iLCJqa3UiOiJodHRwczovL3Nzby5hdXRoLndheWZhaXIuY29tLy53ZWxsLWtub3duL29wZW5pZC1jb25maWd1cmF0aW9uL2p3a3MifQ.eyJuYmYiOjE2NjkyNTA1MjAsImV4cCI6MTY2OTMzNjkyMSwiaWF0IjoxNjY5MjUwNTIxLCJpc3MiOiJodHRwczovL3Nzby5hdXRoLndheWZhaXIuY29tLyIsImF1ZCI6Imh0dHBzOi8vYXBpLndheWZhaXIuY29tLyIsImNsaWVudF9pZCI6IkVDQkMzOTI0LTU1MjUtNDgxNS1BQ0VFLUFGQTYxODZBN0U3QSIsImNsaWVudF9scnBzIjoiMTAwMCIsImNsaWVudF9uYW1lIjoiSW52ZW50b3J5IEFQUCIsInN1YiI6IkVDQkMzOTI0LTU1MjUtNDgxNS1BQ0VFLUFGQTYxODZBN0U3QUBjbGllbnRzIiwic2NvcGUiOiJhdXRoLndheWZhaXIuY29tOmRlZmF1bHRfc2NvcGUgYXV0aC53YXlmYWlyLmNvbTpkZWZhdWx0X3Njb3BlMSByZWFkOmNhc3RsZWdhdGVfc2Nfb3JkZXJzIHZlcmlmaWVkOmFkdmFuY2Vfc2hpcF9ub3RpY2Ugd3JpdGU6Y2FzdGxlZ2F0ZV9zY19vcmRlcnMifQ.TCQlN7fdH10VdsFyStiKiHtOVxDd1nyYEtPhO39esPszmy_mdFMkWczYlB_f7ZEz1YBQdVc3jwSnfUtATTtQWHWEGNHvDE7yFbXY622SV11Sr_7Qm0UksoRWV_Oxalaem0_qRS_jDbPhyI9tw7wsi3jBUMUF27l3onUReXxfq1gukZl0Mk_qBeHw-_v-fiGpnXUY7T5xbt-kuzZkGbWcCeCUuIRCUguwKYsyM7UYYuF1lV2EsW-MXIzmhbNpW3RapwZoc3jviuzmASTDzToYFKVPKEBlM7jIYkg3H6hjv4yvI1Q6Au8ktsWuJhJV4VdmJiDbV0V_5OFU3LZHwoengaop5TaJFq1ieBanYTAGJ-NIT2EVigsudXdMHJRcaFr6C99OU10BbMZh9BQxBb3sz9ZFbERQfWMdC_YhVYbhewSXYLWV8yfDMMkj-8wjxuphNPvQ8KbPeCs3rL8443svcwfYUqXbDduF9OsIcaYzhZr6xOaE7T-rDzMy_ZrJ-cyKCjwv3lrDQy8-W7KGIY-hy7jGH-0p5nhBYI99kmW-KKIv3b4TlkA5YrBSbvX_AawHCq6HhnCkw4NkV6RShqu94Kxh_qlzIHhHsfu02Q9LY4kbrGJZwaTesANNfQpNa6ULUTnjlIkrqXSMDtjKxvbnNyAxvW-5NVAgKoZchQsR7sE");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Cookie", "CSN=g_countryCode%3DUS%26g_zip%3D67346");
            var body = @"{
" + "\n" +
            @"      ""grant_type"":""client_credentials"",
" + "\n" +
            @"      ""client_id"": ""ECBC3924-5525-4815-ACEE-AFA6186A7E7A"",
" + "\n" +
            @"      ""client_secret"": ""h-noOYCouEFATaCHES-jCcaosaTRkxGbLtC9Gj1FM56uj9csGbjpVNx2QjnXiuY7V1hkkMMfcKNNFttAU-gfSQ"",
" + "\n" +
            @"      ""audience"": ""https://api.wayfair.com/""
" + "\n" +
            @"}";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            var response = await client.ExecuteAsync(request);
            Console.WriteLine(response.Content);

        }


    }
}

using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Volo.Abp.DependencyInjection;

namespace Tiger.Infrastructure.RestSharp
{
    public class RestSharpGettingStart:ITransientDependency
    {


        public async void TestRequest()
        {
            var client = new RestClient("https://api.twitter.com/1.1")
            {
                Authenticator = new HttpBasicAuthenticator("username", "password")
            };
            var request = new RestRequest("statuses/home_timeline.json");
            var response = await client.GetAsync(request);
        }


        

    }
}

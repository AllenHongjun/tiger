using RestSharp;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tiger.Infrastructure.Rest.Twitter
{
    public class TwitterClient : ITwitterClient, IDisposable
    {
        readonly RestClient _client;

        public TwitterClient(string apiKey, string apiKeySecret)
        {
            var options = new RestClientOptions("https://api.twitter.com/2");

            _client = new RestClient(options)
            {
                Authenticator = new TwitterAuthenticator("https://api.twitter.com",
                                                         apiKey,
                                                         apiKeySecret)
            };
        }

        public async Task<TwitterUser> GetUser(string user)
        {
            var response = await _client.GetJsonAsync<TwitterUser>(
                "users/by/username/{user}",
                new { user }
            );
            return response;
        }

        //public async IAsyncEnumerable<SearchResponse> SearchStream(
        //    [EnumeratorCancellation] CancellationToken cancellationToken = default
        //)
        //{
        //    var response = _client.StreamJsonAsync<TwitterSingleObject<SearchResponse>>(
        //        "tweets/search/stream", cancellationToken
        //    );

        //    await foreach (var item in response.WithCancellation(cancellationToken))
        //    {
        //        yield return item.Data;
        //    }
        //}

        //record TwitterSingleObject<T>(T Data);

        public void Dispose()
        {
            _client?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}

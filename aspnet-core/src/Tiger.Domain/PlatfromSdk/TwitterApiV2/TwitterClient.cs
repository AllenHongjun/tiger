using RestSharp.Authenticators;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tiger.PlatfromSdk.TwitterApiV2
{
    public interface ITwitterClient
    {
        Task<TwitterUser> GetUser(string user);
    }

    public class TwitterClient : ITwitterClient, IDisposable
    {
        readonly RestClient _client;

        public TwitterClient(string apiKey, string apiKeySecret)
        {
            var options = new RestClientOptions("https://api.twitter.com/2");

            _client = new RestClient(options)
            {
                Authenticator = new TwitterAuthenticator("https://api.twitter.com", apiKey, apiKeySecret)
            };
        }

        public async Task<TwitterUser> GetUser(string user)
        {
            var response = await _client.GetJsonAsync<TwitterSingleObject<TwitterUser>>(
                "users/by/username/{user}",
                new { user }
            );
            return response!.Data;
        }


        public class TwitterSingleObject<T>{
            public T Data;
        }

        public void Dispose()
        {
            _client?.Dispose();
            GC.SuppressFinalize(this);
        }
    }



    public class TwitterAuthenticator : AuthenticatorBase
    {
        readonly string _baseUrl;
        readonly string _clientId;
        readonly string _clientSecret;

        public TwitterAuthenticator(string baseUrl, string clientId, string clientSecret) : base("")
        {
            _baseUrl      = baseUrl;
            _clientId     = clientId;
            _clientSecret = clientSecret;
        }

        protected override async ValueTask<Parameter> GetAuthenticationParameter(string accessToken)
        {
            var token = string.IsNullOrEmpty(Token) ? await GetToken() : Token;
            return new HeaderParameter(KnownHeaders.Authorization, token);
        }

        async Task<string> GetToken()
        {
            var options = new RestClientOptions(_baseUrl);

            using var client = new RestClient(options)
            {
                Authenticator = new HttpBasicAuthenticator(_clientId, _clientSecret),
            };

            var request = new RestRequest("oauth2/token")
                .AddParameter("grant_type", "client_credentials");
            var response = await client.PostAsync<TokenResponse>(request);
            return $"{response!.TokenType} {response!.AccessToken}";
        }

        record TokenResponse
        {
            [JsonPropertyName("token_type")]
            public string TokenType { get; set; }
            [JsonPropertyName("access_token")]
            public string AccessToken { get; set; }
        }
    }

    public class TwitterUser
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
    }
}

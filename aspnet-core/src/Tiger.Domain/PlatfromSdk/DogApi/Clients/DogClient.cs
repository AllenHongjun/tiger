using Newtonsoft.Json;
using RestSharp;
using System.Diagnostics;
using System.Threading.Tasks;
using Tiger.PlatfromSdk.DogApi.Clients.Interfaces;
using Tiger.PlatfromSdk.DogApi.Models;
using Volo.Abp.DependencyInjection;

namespace Tiger.PlatfromSdk.DogApi.Clients
{
    public class DogClient : IDogClient, ISingletonDependency
    {   
        private readonly RestClient _client;

        public DogClient()
        {
            var options = new RestClientOptions("https://dog.ceo");
            _client = new RestClient(options);
        }

        public async Task<ListAllBreedsResponse> GetAllBreeds()
        {
            var request = new RestRequest("/api/breeds/list/all", Method.Get);
            var response = await _client.ExecuteAsync<ListAllBreedsResponse>(request);

            //Trace.Write(string.Format("Request completed in {0} ms, Request: {1}, Response: {2}",
            //    10,
            //    JsonConvert.SerializeObject(request),
            //    JsonConvert.SerializeObject(response)));
            return response.Data;
        }

        #region By Breed
        public async Task<MessageReponse> GetImagesByBreed(string breed = "hound")
        {
            var request = new RestRequest("/api/breed/{breed}/images", Method.Get)
                .AddUrlSegment("breed", breed);
            var response = await _client.ExecuteAsync<MessageReponse>(request);
            return response.Data;
        }

        public async Task<MessageReponse> GetRandomImageByBreed(string breed = "hound")
        {
            var request = new RestRequest("/api/breed/{breed}/images/random", Method.Get)
                .AddUrlSegment("breed", breed); ;
            var response = await _client.ExecuteAsync<MessageReponse>(request);
            return response.Data;
        }

        public async Task<MessageReponse> GetMutilpleRandomImagesByBreed(string breed = "hound",int number = 50)
        {
            var request = new RestRequest("/api/breed/{breed}/images/random/{number}", Method.Get)
                .AddUrlSegment("breed", breed)
                .AddUrlSegment("number", number);
            var response = await _client.ExecuteAsync<MessageReponse>(request);
            return response.Data;
        }
        #endregion

        #region BySubBreed
        public async Task<MessageReponse> GetAllSubBreeds(string breed = "bulldog")
        {
            var request = new RestRequest("/api/breed/{subBreed}/list", Method.Get)
                .AddUrlSegment("breed", breed);
            var response = await _client.ExecuteAsync<MessageReponse>(request);
            return response.Data;
        }

        public async Task<MessageReponse> GetSingleRadomImageBySubBreed(string breed, string subbreed)
        {
            var request = new RestRequest("/api/breed/{breed}/{subbreed}/images", Method.Get)
                .AddUrlSegment("breed", breed)
                .AddUrlSegment("subbreed", subbreed);
            var response = await _client.ExecuteAsync<MessageReponse>(request);
            //Trace.TraceError(string.Format("Request completed in {0} ms, Request: {1}, Response: {2}",
            //    10,
            //    JsonConvert.SerializeObject(request),
            //    JsonConvert.SerializeObject(response)));
            return response.Data;
        }

        public async Task<MessageReponse> GetImagesBySubBreed(string breed, string subbreed)
        {
            var request = new RestRequest("/api/breed/{breed}/{subbreed}/images", Method.Get)
                .AddUrlSegment("breed", breed)
                .AddUrlSegment("subbreed", subbreed);
            var response = await _client.ExecuteAsync<MessageReponse>(request);
            return response.Data;
        }

        public async Task<MessageReponse> GetSingleRadomImageByBreed(string breed, string subbreed)
        {
            var request = new RestRequest("/breed/{breed}/{subbreed}/images/random", Method.Get)
                .AddUrlSegment("breed", breed)
                .AddUrlSegment("subbreed", subbreed);
            var response = await _client.ExecuteAsync<MessageReponse>(request);
            return response.Data;
        }

        public async Task<MessageReponse> GetMutilpleRandomImagesBySubBreed(string breed, string subbreed)
        {
            var request = new RestRequest("/breed/{breed}/{subbreed}/images/random/3", Method.Get)
                .AddUrlSegment("breed", breed)
                .AddUrlSegment("subbreed", subbreed);
            var response = await _client.ExecuteAsync<MessageReponse>(request);
            return response.Data;
        }

        #endregion
        
    }
}

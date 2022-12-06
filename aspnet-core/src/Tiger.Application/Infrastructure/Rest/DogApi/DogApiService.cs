using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestSharp.Authenticators;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using Tiger.PlatfromSdk.DogApi.Clients.Interfaces;
using Volo.Abp;
using System.Threading.Tasks;
using Tiger.PlatfromSdk.DogApi.Models;

namespace Tiger.Infrastructure.Rest.DogApi
{
    [ApiExplorerSettings(GroupName = "admin")]
    [RemoteService(false)]
    public class DogApiService : TigerAppService
    {
        private readonly ILogger _logger;
        private readonly IDogClient _dogClient;

        public DogApiService(ILogger<DogApiService> logger, IDogClient dogClient)
        {
            _logger=logger;
            _dogClient=dogClient;
        }


        [HttpGet]
        public async Task<MessageReponse>  TestRequest()
        {
            //var response =  await _dogClient.GetAllBreeds();
            //var r1 = await _dogClient.GetRandomImageByBreed();
            //var r2 = await _dogClient.GetImagesByBreed();
            //var r3 = await _dogClient.GetMutilpleRandomImagesByBreed();


            //var r4 = await _dogClient.GetAllSubBreeds();
            //var r5 = await _dogClient.GetImagesBySubBreed("hound", "english");
            //var r6 = await _dogClient.GetMutilpleRandomImagesBySubBreed("hound", "english");
            var r7 = await _dogClient.GetSingleRadomImageBySubBreed("hound", "english");


            //_logger.LogInformation("GetAllBreeds", response);
            return r7;
        }
    }
}

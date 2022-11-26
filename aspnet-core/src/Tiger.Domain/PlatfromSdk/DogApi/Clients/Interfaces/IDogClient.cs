using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.PlatfromSdk.DogApi.Models;

namespace Tiger.PlatfromSdk.DogApi.Clients.Interfaces
{
    public interface IDogClient
    {
        Task<ListAllBreedsResponse> GetAllBreeds();
        Task<MessageReponse> GetAllSubBreeds(string breed = "bulldog");
        Task<MessageReponse> GetImagesByBreed(string breed = "hound");
        Task<MessageReponse> GetImagesBySubBreed(string breed, string subbreed);
        Task<MessageReponse> GetMutilpleRandomImagesByBreed(string breed = "hound", int number = 50);
        Task<MessageReponse> GetMutilpleRandomImagesBySubBreed(string breed, string subbreed);
        Task<MessageReponse> GetRandomImageByBreed(string breed = "hound");
        Task<MessageReponse> GetSingleRadomImageByBreed(string breed, string subbreed);
        Task<MessageReponse> GetSingleRadomImageBySubBreed(string breed, string subbreed);
    }
}

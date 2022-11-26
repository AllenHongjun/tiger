using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.PlatfromSdk.DogApi.Models
{
    public class MessageReponse
    {
        public class BaseResponse
        {
            public string[] message { get; set; }
            public string status { get; set; }
        }
    }
}

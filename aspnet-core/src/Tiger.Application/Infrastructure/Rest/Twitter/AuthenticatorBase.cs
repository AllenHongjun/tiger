using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tiger.Infrastructure.Rest.Twitter
{
    public abstract class AuthenticatorBase: IAuthenticator
    {
        private string v;

        protected AuthenticatorBase(string v)
        {
            this.v=v;
        }

        public string Token { get; set; }

        public ValueTask Authenticate(RestClient client, RestRequest request)
        {
            throw new NotImplementedException();
        }

        public abstract ValueTask<Parameter> GetAuthenticationParameter(string accessToken);
    }
}

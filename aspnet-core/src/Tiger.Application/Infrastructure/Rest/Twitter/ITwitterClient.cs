using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tiger.Infrastructure.Rest.Twitter
{
    public interface ITwitterClient
    {
        Task<TwitterUser> GetUser(string user);
    }
}

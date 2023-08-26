using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Tiger.Module.System.Server
{
    public interface IServerAppService
    {
        dynamic GetServerBase();
        dynamic GetServerDisk();
        dynamic GetServerUsed();
    }
}

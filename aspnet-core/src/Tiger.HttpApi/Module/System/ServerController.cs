using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Tiger.Module.System.Cache.Permissions;
using Tiger.Module.System.Cache;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Tiger.Module.System.Server;

namespace Tiger.Module.System
{   
    /// <summary>
    /// 获取服务器监控信息
    /// </summary>
    [Controller]
    [RemoteService(Name = "ServerMonitor")]
    [Route("api/monitor/server")]
    public class ServerController : AbpController, IServerAppService
    {
        public ServerController(IServerAppService serverAppService)
        {
            ServerAppService=serverAppService;
        }

        protected IServerAppService ServerAppService { get; }

        /// <summary>
        /// 获取服务器基本信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("server-base")]
        public dynamic GetServerBase()
        {
            
            return ServerAppService.GetServerBase();
        }

        /// <summary>
        /// 磁盘信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("server-disk")]
        public dynamic GetServerDisk()
        {
            return ServerAppService.GetServerDisk();
        }

        /// <summary>
        /// 服务器使用信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("server-used")]
        public dynamic GetServerUsed()
        {
            return ServerAppService.GetServerUsed();
        }
    }
}

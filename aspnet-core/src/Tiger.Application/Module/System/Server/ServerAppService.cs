using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using Volo.Abp;
using Volo.Abp.Application.Services;

namespace Tiger.Module.System.Server
{

    /// <summary>
    /// 获取服务器信息
    /// </summary>
    [RemoteService(isEnabled: false)]
    public class ServerAppService : ApplicationService, IServerAppService
    {
        public ServerAppService(IWebHostEnvironment env)
        {
            this.env=env;
        }

        protected IWebHostEnvironment env { get; }

        /// <summary>
        /// 获取服务器配置信息
        /// </summary>
        /// <returns></returns>
        [DisplayName("获取服务器配置信息")]
        public dynamic GetServerBase()
        {

            var addressList = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
            //var ip = addressList.FirstOrDefault(address => address.AddressFamily == Sockets.AddressFamily.InterNetwork)?.ToString();

            return new
            {
                HostName = Environment.MachineName, // 主机名称
                SystemOs = RuntimeInformation.OSDescription, // 操作系统
                OsArchitecture = Environment.OSVersion.Platform.ToString() + " " + RuntimeInformation.OSArchitecture.ToString(), // 系统架构
                ProcessorCount = Environment.ProcessorCount + " 核", // CPU核心数
                SysRunTime = DateTime.UtcNow, // 系统运行时间
                RemoteIp = ServerInfoUtil.GetIpFromOnline(), // 外网地址
                //LocalIp = addressList.FirstOrDefault(), // 本地地址
                RuntimeInformation.FrameworkDescription, // NET框架
                Environment = env.IsDevelopment() ? "Development" : "Production",
                Stage = env.IsStaging() ? "Stage环境" : "非Stage环境", // 是否Stage环境
            };
        }


        /// <summary>
        /// 获取服务器使用信息
        /// </summary>
        /// <returns></returns>
        [DisplayName("获取服务器使用信息")]
        public dynamic GetServerUsed()
        {
            var programStartTime = Process.GetCurrentProcess().StartTime;
            var totalMilliseconds = (DateTime.Now - programStartTime).TotalMilliseconds.ToString();
            var ts = totalMilliseconds.Contains('.') ? totalMilliseconds.Split('.')[0] : totalMilliseconds;
            //var programRunTime = DateTimeUtil.FormatTime(ts.ParseToLong());

            var memoryMetrics = ServerInfoUtil.GetComputerInfo();
            return new
            {
                memoryMetrics.FreeRam, // 空闲内存
                memoryMetrics.UsedRam, // 已用内存
                memoryMetrics.TotalRam, // 总内存
                memoryMetrics.RamRate, // 内存使用率
                memoryMetrics.CpuRate, // Cpu使用率
                StartTime = programStartTime.ToString("yyyy-MM-dd HH:mm:ss"), // 服务启动时间
                //RunTime = programRunTime, // 服务运行时间
            };
        }

        /// <summary>
        /// 获取服务器磁盘信息
        /// </summary>
        /// <returns></returns>
        [DisplayName("获取服务器磁盘信息")]
        public dynamic GetServerDisk()
        {
            return ServerInfoUtil.GetDiskInfos();
        }

        /// <summary>
        /// 获取框架主要程序集
        /// </summary>
        /// <returns></returns>
        [DisplayName("获取框架主要程序集")]
        public dynamic GetAssemblyList()
        {
            var abpAssembly = typeof(AbpApplicationBase).Assembly.GetName();
            

            return new[]
            {
                new { abpAssembly.Name, abpAssembly.Version },
            };
        }
    }
}

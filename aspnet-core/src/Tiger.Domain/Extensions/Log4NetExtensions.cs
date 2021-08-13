/**
 * 类    名：Log4NetExtensions   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/11 23:35:39       
 * 说    明: 
 * 
 */

using log4net;
using log4net.Config;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Tiger.Extensions
{
    public static class Log4NetExtensions
    {
        public static IHostBuilder UseLog4Net(this IHostBuilder hostBuilder)
        {
            var log4netRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(log4netRepository, new FileInfo("./Config/log4net.config"));

            return hostBuilder;
        }
    }
}

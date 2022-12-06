using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;

namespace Tiger.Infrastructure.Logger
{
    [ApiExplorerSettings(GroupName = "admin")]
    [RemoteService(false)]
    public class AboutModel: TigerAppService
    {
        // ElasticSearch+Kibana 实现日志可视化 和搜索引擎使用。


        // 创建一个记录器 ILogger<AboutModel>，该记录器使用类型为 AboutModel 的完全限定名称的日志类别。 
        private readonly ILogger _logger;

        public AboutModel(ILogger<AboutModel> logger)
        {
            _logger = logger;
        }
        public string Message { get; set; }

        public void OnGet()
        {   
            Message = $"About page visited at {DateTime.UtcNow.ToLongTimeString()}";

            // 调用 LogInformation 以在 Information 级别登录。 日志“级别”代表所记录事件的严重程度
            // 记录4个等级的日志

            _logger.LogCritical(Message);
            _logger.LogInformation(Message);
            _logger.LogWarning(Message);
            _logger.LogDebug(Message);
            _logger.LogError(Message);
            _logger.LogTrace(Message);
        }
    }
}

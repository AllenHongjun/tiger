using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.ExceptionHandling;
using Volo.Abp.Logging;
using Volo.Abp;

namespace Tiger.Infrastructure.CloudAliyun.Aliyun
{
    public class AbpAliyunException : AbpException, IHasErrorCode, IHasLogLevel
    {
        public LogLevel LogLevel { get; set; }

        public string Code { get; }

        public AbpAliyunException(string code, string message)
            : base(message)
        {
            Code = code;
            LogLevel = LogLevel.Warning;
        }
    }
}

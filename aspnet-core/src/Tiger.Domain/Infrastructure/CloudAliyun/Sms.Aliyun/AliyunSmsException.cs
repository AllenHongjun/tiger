using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Infrastructure.CloudAliyun.Aliyun;

namespace Tiger.Infrastructure.CloudAliyun.Sms.Aliyun
{
    public class AliyunSmsException : AbpAliyunException
    {
        public AliyunSmsException(string code, string message) 
            : base(code, message)
        {
        }
    }
}

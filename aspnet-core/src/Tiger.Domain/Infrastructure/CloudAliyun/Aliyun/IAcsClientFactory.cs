﻿using Aliyun.Acs.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tiger.Infrastructure.CloudAliyun.Aliyun
{
    public interface IAcsClientFactory
    {
        /// <summary>
        /// 构造一个通用的Acs客户端调用
        /// 通过CommonRequest调用可以不需要集成其他SDK包
        /// </summary>
        /// <returns></returns>
        Task<IAcsClient> CreateAsync();
    }
}

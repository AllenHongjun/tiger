using Aliyun.OSS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tiger.Infrastructure.CloudAliyun.BlobStoring.Aliyun
{
    public interface IOssClientFactory
    {

        /// <summary>
        /// 构建Oss客户端
        /// </summary>
        /// <typeparam name="TContainer"></typeparam>
        /// <returns></returns>
        Task<IOss> CreateAsync<TContainer>();

        /// <summary>
        /// 通过配置信息构建Oss客户端调用
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        Task<IOss> CreateAsync(AliyunBlobProviderConfiguration configuration);

    }
}

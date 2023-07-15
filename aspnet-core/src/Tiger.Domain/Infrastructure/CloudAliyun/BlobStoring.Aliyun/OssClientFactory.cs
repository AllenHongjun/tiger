using Aliyun.OSS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Infrastructure.CloudAliyun.Aliyun;
using Volo.Abp.BlobStoring;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Settings;

namespace Tiger.Infrastructure.CloudAliyun.BlobStoring.Aliyun
{
    public class OssClientFactory : AliyunClientFactory<IOss, AliyunBlobProviderConfiguration>, IOssClientFactory, ITransientDependency
    {
        protected IBlobContainerConfigurationProvider ConfigurationProvider { get; }
        public OssClientFactory(
            ISettingProvider settingProvider,
            IBlobContainerConfigurationProvider configurationProvider,
            IDistributedCache<AliyunBasicSessionCredentialsCacheItem> cache)
            : base(settingProvider, cache)
        {
            ConfigurationProvider = configurationProvider;
        }

        public async virtual Task<IOss> CreateAsync<TContainer>()
        {
            var configuration = ConfigurationProvider.Get<TContainer>();

            return await CreateAsync(configuration.GetAliyunConfiguration());
        }
        /// <summary>
        /// 普通方式构建Oss客户端
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="regionId"></param>
        /// <param name="accessKeyId"></param>
        /// <param name="accessKeySecret"></param>
        /// <returns></returns>
        protected override IOss GetClient(AliyunBlobProviderConfiguration configuration, string regionId, string accessKeyId, string accessKeySecret)
        {
            return new OssClient(
                configuration.Endpoint,
                accessKeyId,
                accessKeySecret);
        }
        /// <summary>
        /// 通过用户安全令牌构建Oss客户端
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="regionId"></param>
        /// <param name="accessKeyId"></param>
        /// <param name="accessKeySecret"></param>
        /// <param name="securityToken"></param>
        /// <returns></returns>
        protected override IOss GetSecurityTokenClient(AliyunBlobProviderConfiguration configuration, string regionId, string accessKeyId, string accessKeySecret, string securityToken)
        {
            return new OssClient(
                configuration.Endpoint,
                accessKeyId,
                accessKeySecret,
                securityToken);
        }
    }
}

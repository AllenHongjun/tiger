using Aliyun.Acs.Core.Auth;
using Aliyun.Acs.Core.Profile;
using Aliyun.Acs.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Settings;
using System.Threading.Tasks;

namespace Tiger.Infrastructure.CloudAliyun.Aliyun
{
    public class AcsClientFactory : AliyunClientFactory<IAcsClient>, IAcsClientFactory, ITransientDependency
    {
        public AcsClientFactory(
            ISettingProvider settingProvider,
            IDistributedCache<AliyunBasicSessionCredentialsCacheItem> cache)
            : base(settingProvider, cache)
        {
        }

        protected override IAcsClient GetClient(string regionId, string accessKeyId, string accessKeySecret)
        {
            return new DefaultAcsClient(
                DefaultProfile.GetProfile(regionId, accessKeyId, accessKeySecret));
        }

        protected override IAcsClient GetSecurityTokenClient(string regionId, string accessKeyId, string accessKeySecret, string securityToken)
        {
            var profile = DefaultProfile.GetProfile(regionId);
            var credentials = new BasicSessionCredentials(accessKeyId, accessKeySecret, securityToken);
            return new DefaultAcsClient(profile, credentials);
        }
        
    }
}

using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace Tiger.Module.OssManagement
{   
    /// <summary>
    /// OSS容器数据初始化对象
    /// </summary>
    public class OssStaticContainerDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly AbpOssManagementOptions _options;
        private readonly IOssContainerFactory _ossContainerFactory;
        public OssStaticContainerDataSeedContributor(
            IOptions<AbpOssManagementOptions> options,
            IOssContainerFactory ossContainerFactory)
        {
            _options = options.Value;
            _ossContainerFactory = ossContainerFactory;
        }

        public async virtual Task SeedAsync(DataSeedContext context)
        {
            var ossContainer = _ossContainerFactory.Create();

            foreach (var bucket in _options.StaticBuckets)
            {
                await ossContainer.CreateIfNotExistsAsync(bucket);
            }
        }
    }
}

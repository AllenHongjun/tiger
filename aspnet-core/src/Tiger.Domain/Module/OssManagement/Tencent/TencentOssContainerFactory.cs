using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Timing;

namespace Tiger.Module.OssManagement.Tencent
{
    public class TencentOssContainerFactory : IOssContainerFactory
    {
        protected IClock Clock { get; }
        protected ICurrentTenant CurrentTenant { get; }
        protected ICosClientFactory CosClientFactory { get; }

        public TencentOssContainerFactory(
            IClock clock,
            ICurrentTenant currentTenant,
            ICosClientFactory cosClientFactory)
        {
            Clock = clock;
            CurrentTenant = currentTenant;
            CosClientFactory = cosClientFactory;
        }

        public IOssContainer Create()
        {
            return new TencentOssContainer(
                Clock,
                CurrentTenant,
                CosClientFactory);
        }
    }
}

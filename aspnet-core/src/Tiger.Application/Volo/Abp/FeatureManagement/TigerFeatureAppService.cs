using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Features;

namespace Tiger.Volo.Abp.FeatureManagement
{
    /// <summary>
    /// 特性管理
    /// </summary>
    /// <remarks>
    /// 只是为了方便查询abp特性管理的源码
    /// </remarks>
    [RemoteService(false)]
    public class TigerFeatureAppService : FeatureAppService
    {   
        protected IFeatureValueRepository FeatureValueRepository { get; }
        public TigerFeatureAppService(
            IFeatureManager featureManager, 
            IFeatureDefinitionManager featureDefinitionManager, 
            IOptions<FeatureManagementOptions> options,
            IFeatureValueRepository featureValueRepository
            ) : base(featureManager, featureDefinitionManager, options)
        {
            FeatureValueRepository = featureValueRepository;
        }
    }
}

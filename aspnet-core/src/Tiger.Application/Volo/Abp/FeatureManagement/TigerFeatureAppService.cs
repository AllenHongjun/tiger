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

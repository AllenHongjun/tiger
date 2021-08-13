/**
 * 类    名：TigerFeatureAppService   
 * 作    者：hongjy       
 * 创建时间：2021/8/13 9:44:36       
 * 说    明: 
 * 
 */

using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Features;

namespace Tiger.Volo.Abp.FeatureManagement
{   
    /// <summary>
    /// 特性管理
    /// </summary>
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

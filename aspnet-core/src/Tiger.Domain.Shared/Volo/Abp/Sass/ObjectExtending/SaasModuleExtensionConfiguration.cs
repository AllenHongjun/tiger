using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.ObjectExtending.Modularity;

namespace Tiger.Volo.Abp.Sass.ObjectExtending
{   
    /// <summary>
    /// 模块扩展配置
    /// </summary>
    /// <remarks>
    /// 作用查看官方文档
    /// </remarks>
    public class SaasModuleExtensionConfiguration : ModuleExtensionConfiguration
    {
        public SaasModuleExtensionConfiguration ConfigureTenant(
        Action<EntityExtensionConfiguration> configureAction)
        {
            return this.ConfigureEntity(
                SaasModuleExtensionConsts.EntityNames.Edition,
                configureAction
            );
        }
    }
}

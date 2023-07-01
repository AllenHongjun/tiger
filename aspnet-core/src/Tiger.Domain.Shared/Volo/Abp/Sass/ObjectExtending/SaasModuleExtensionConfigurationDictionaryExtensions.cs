using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.ObjectExtending.Modularity;

namespace Tiger.Volo.Abp.Sass.ObjectExtending
{
    public static class SaasModuleExtensionConfigurationDictionaryExtensions
    {   
        /// <summary>
        /// 扩展方法
        /// </summary>
        /// <param name="modules"></param>
        /// <param name="configureAction"></param>
        /// <returns></returns>
        public static ModuleExtensionConfigurationDictionary ConfigureTenantManagement(
        this ModuleExtensionConfigurationDictionary modules,
        Action<SaasModuleExtensionConfiguration> configureAction)
        {
            return modules.ConfigureModule(
                SaasModuleExtensionConsts.ModuleName,
                configureAction
            );
        }
    }
}

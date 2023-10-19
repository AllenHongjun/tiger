using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Module.System.TextTemplate.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Tiger.Module.System.Area.Permissions
{   
    /// <summary>
    /// 定义文本模板权限
    /// </summary>
    public class AbpAreaPermissionDefinitionProvider: PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {   
            // 文本模板权限组
            var textTemplatingGroup = context
                .AddGroup(AbpAreaPermissions.GroupName, L("Permission:Area"));

            // 文本模板管理权限
            var textTemplatePermission = textTemplatingGroup
                .AddPermission(AbpAreaPermissions.Region.Default, L("Permission:Regions"));
            // 文本模板管理子权限 增删改查
            textTemplatePermission.AddChild(AbpAreaPermissions.Region.Create, L("Permission:Create"));
            textTemplatePermission.AddChild(AbpAreaPermissions.Region.Update, L("Permission:Edit"));
            textTemplatePermission.AddChild(AbpAreaPermissions.Region.Delete, L("Permission:Delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<AbpTextTemplateResource>(name);
        }
    }
}

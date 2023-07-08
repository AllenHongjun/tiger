using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Module.System.TextTemplate.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Tiger.Module.System.TextTemplate.Permissions
{   
    /// <summary>
    /// 定义文本模板权限
    /// </summary>
    public class AbpTextTemplatePermissionDefinitionProvider: PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var textTemplatingGroup = context.AddGroup(AbpTextTemplatePermissions.GroupName, L("Permission:TextTemplating"));

            var textTemplatePermission = textTemplatingGroup.AddPermission(AbpTextTemplatePermissions.TextTemplate.Default, L("Permission:TextTemplates"));
            textTemplatePermission.AddChild(AbpTextTemplatePermissions.TextTemplate.Create, L("Permission:Create"));
            textTemplatePermission.AddChild(AbpTextTemplatePermissions.TextTemplate.Update, L("Permission:Edit"));
            textTemplatePermission.AddChild(AbpTextTemplatePermissions.TextTemplate.Delete, L("Permission:Delete"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<AbpTextTemplateResource>(name);
        }
    }
}

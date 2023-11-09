using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Module.TaskManagement.Localization;
using Tiger.Module.TaskManagement.Permissions;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Tiger.Module.QuestionBank.Permissions
{
    public class QuestionBankPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var group = context.AddGroup(
                QuestionBankPermissions.GroupName,
                L("Permissions:QuestionBank"));

            var backgroundJobs = group.AddPermission(
                QuestionBankPermissions.QuestionCategory.Default,
                L("Permissions:QuestionCategory"));
            backgroundJobs.AddChild(
                QuestionBankPermissions.QuestionCategory.Create,
                L("Permissions:Create"));
            backgroundJobs.AddChild(
                QuestionBankPermissions.QuestionCategory.Update,
                L("Permissions:Update"));
            backgroundJobs.AddChild(
                QuestionBankPermissions.QuestionCategory.Delete,
                L("Permissions:Delete"));
        }

        private ILocalizableString L(string name)
        {
            return LocalizableString.Create<TaskManagementResource>(name);
        }
    }
}

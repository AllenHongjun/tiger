using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Module.QuestionBank.Localization;
using Tiger.Module.TaskManagement.Localization;
using Tiger.Module.TaskManagement.Permissions;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Tiger.Module.QuestionBank.Permissions
{
    public class QuestionBankPermissionDefinitionProvider:PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var group = context.AddGroup(
                QuestionBankPermissions.GroupName,
                L("Permissions:QuestionBank"));

            var questionCategory = group.AddPermission(
                QuestionBankPermissions.QuestionCategory.Default,
                L("Permissions:QuestionCategory"));
                questionCategory.AddChild(
                        QuestionBankPermissions.QuestionCategory.Create,
                        L("Permissions:Create"));
                questionCategory.AddChild(
                        QuestionBankPermissions.QuestionCategory.Update,
                        L("Permissions:Update"));
                questionCategory.AddChild(
                        QuestionBankPermissions.QuestionCategory.Delete,
                        L("Permissions:Delete"));

            var question = group.AddPermission(
                QuestionBankPermissions.Question.Default,
                L("Permissions:Question"));
                question.AddChild(
                    QuestionBankPermissions.Question.Create,
                    L("Permissions:Create"));
                question.AddChild(
                    QuestionBankPermissions.Question.Update,
                    L("Permissions:Update"));
                question.AddChild(
                    QuestionBankPermissions.Question.Delete,
                    L("Permissions:Delete"));
        }

        private ILocalizableString L(string name)
        {
            return LocalizableString.Create<QuestionBankResources>(name);
        }
    }
}

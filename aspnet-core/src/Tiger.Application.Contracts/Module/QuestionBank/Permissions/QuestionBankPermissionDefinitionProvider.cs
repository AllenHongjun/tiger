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
                L("Permission:QuestionBank"));

            var questionCategory = group.AddPermission(QuestionBankPermissions.QuestionCategory.Default,L("Permission:QuestionCategory"));
            questionCategory.AddChild(QuestionBankPermissions.QuestionCategory.Create,L("Permission:Create"));
            questionCategory.AddChild(QuestionBankPermissions.QuestionCategory.Update,L("Permission:Update"));
            questionCategory.AddChild(QuestionBankPermissions.QuestionCategory.Delete,L("Permission:Delete"));
            questionCategory.AddChild(QuestionBankPermissions.QuestionCategory.Export, L("Permission:Export"));
            questionCategory.AddChild(QuestionBankPermissions.QuestionCategory.Import, L("Permission:Import"));

            var question = group.AddPermission(QuestionBankPermissions.Question.Default,L("Permission:Question"));
            question.AddChild(QuestionBankPermissions.Question.Create,L("Permission:Create"));
            question.AddChild(QuestionBankPermissions.Question.Update,L("Permission:Update"));
            question.AddChild(QuestionBankPermissions.Question.Delete,L("Permission:Delete"));

            var trainPlatform = group.AddPermission(QuestionBankPermissions.TrainPlatform.Default,L("Permission:TrainPlatform"));
            trainPlatform.AddChild(QuestionBankPermissions.TrainPlatform.Create,L("Permission:Create"));
            trainPlatform.AddChild(QuestionBankPermissions.TrainPlatform.Update,L("Permission:Update"));
            trainPlatform.AddChild(QuestionBankPermissions.TrainPlatform.Delete,L("Permission:Delete"));
        }

        private ILocalizableString L(string name)
        {
            return LocalizableString.Create<QuestionBankResources>(name);
        }
    }
}

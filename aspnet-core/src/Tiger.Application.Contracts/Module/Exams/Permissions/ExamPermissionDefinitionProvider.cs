using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Module.QuestionBank.Localization;
using Tiger.Module.QuestionBank.Permissions;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Tiger.Module.Exams.Permissions
{
    public class ExamPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var group = context.AddGroup(
                ExamPermissions.GroupName,
                L("Permissions:Exam"));

            var exam = group.AddPermission(
                ExamPermissions.Exam.Default,
                L("Permissions:Exam"));
            exam.AddChild(
                    ExamPermissions.Exam.Create,
                    L("Permissions:Create"));
            exam.AddChild(
                    ExamPermissions.Exam.Update,
                    L("Permissions:Update"));
            exam.AddChild(
                    ExamPermissions.Exam.Delete,
                    L("Permissions:Delete"));

            var testPaper = group.AddPermission(
                ExamPermissions.TestPaper.Default,
                L("Permissions:TestPaper"));
            testPaper.AddChild(
                    ExamPermissions.TestPaper.Create,
                    L("Permissions:Create"));
            testPaper.AddChild(
                    ExamPermissions.TestPaper.Update,
                    L("Permissions:Update"));
            testPaper.AddChild(
                    ExamPermissions.TestPaper.Delete,
                    L("Permissions:Delete"));

            var testPaperStrategy = group.AddPermission(
                ExamPermissions.TestPaperStrategy.Default,
                L("Permissions:TestPaperStrategy"));
            testPaperStrategy.AddChild(
                    ExamPermissions.TestPaperStrategy.Create,
                    L("Permissions:Create"));
            testPaperStrategy.AddChild(
                    ExamPermissions.TestPaperStrategy.Update,
                    L("Permissions:Update"));
            testPaperStrategy.AddChild(
                    ExamPermissions.TestPaperStrategy.Delete,
                    L("Permissions:Delete"));

            var testPaperQuestion = group.AddPermission(
                ExamPermissions.TestPaperQuestion.Default,
                L("Permissions:TestPaperQuestion"));
            testPaperQuestion.AddChild(
                    ExamPermissions.TestPaperQuestion.Create,
                    L("Permissions:Create"));
            testPaperQuestion.AddChild(
                    ExamPermissions.TestPaperQuestion.Update,
                    L("Permissions:Update"));
            testPaperQuestion.AddChild(
                    ExamPermissions.TestPaperQuestion.Delete,
                    L("Permissions:Delete"));

            var answerSheet = group.AddPermission(
                ExamPermissions.AnswerSheet.Default,
                L("Permissions:AnswerSheet"));
            answerSheet.AddChild(
                    ExamPermissions.AnswerSheet.Create,
                    L("Permissions:Create"));
            answerSheet.AddChild(
                    ExamPermissions.AnswerSheet.Update,
                    L("Permissions:Update"));
            answerSheet.AddChild(
                    ExamPermissions.AnswerSheet.Delete,
                    L("Permissions:Delete"));

            var answerSheetDetail = group.AddPermission(
                ExamPermissions.AnswerSheetDetail.Default,
                L("Permissions:AnswerSheetDetail"));
            answerSheetDetail.AddChild(
                    ExamPermissions.AnswerSheetDetail.Create,
                    L("Permissions:Create"));
            answerSheetDetail.AddChild(
                    ExamPermissions.AnswerSheetDetail.Update,
                    L("Permissions:Update"));
            answerSheetDetail.AddChild(
                    ExamPermissions.AnswerSheetDetail.Delete,
                    L("Permissions:Delete"));



        }

        private ILocalizableString L(string name)
        {
            return LocalizableString.Create<QuestionBankResources>(name);
        }
    }
}

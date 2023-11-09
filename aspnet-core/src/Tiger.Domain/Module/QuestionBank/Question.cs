using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Tiger.Module.QuestionBank
{
    /// <summary>
    /// 题目表
    /// </summary>
    [Table("Question")]
    public class Question:FullAuditedAggregateRoot<Guid>,IMultiTenant
    {
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 题库
        /// </summary>
        public Guid QuestionCategoryId { get; set; }

        /// <summary>
        /// 实训平台Id
        /// </summary>
        public Guid? TrainPlatformId { get; set; }

        /// <summary>
        /// 题型 1.判断 2.单选 3.多选 4.填空 5.计算题 6.问答题 7.B型题,8.简答题 9.实训任务
        /// </summary>
        public QuestionType Type { get; set; }

        /// <summary>
        /// 题目名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 题目内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 选项内容（用|分隔）
        /// </summary>
        [Obsolete("使用单个选项填充")]
        [NotMapped]
        public string OptionContent { get; set; }

        /// <summary>
        /// 选项数量
        /// </summary>
        [Obsolete]
        [NotMapped]
        public int? OptionSize { get; set; }

        /// <summary>
        /// A选项
        /// </summary>
        public string OptionA { get; set; }
        public string OptionB { get; set; }
        public string OptionC { get; set; }
        public string OptionD { get; set; }
        public string OptionE { get; set; }

        /// <summary>
        /// 答案
        /// </summary>
        [Obsolete("使用QuestionAnswer表替代")]
        public string Answer { get; set; }

        /// <summary>
        /// 难易度：1.简单 2.普通 3.困难
        /// </summary>
        public QuestionDegree Degree { get; set; }

        /// <summary>
        /// 试题解析
        /// </summary>
        public string Analysis { get; set; }

        /// <summary>
        /// 出处
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// 帮助文本
        /// </summary>
        public string HelpMessage { get; set; }

        /// <summary>
        /// 帮助视频
        /// </summary>
        public string HelpVideo { get; set; }

        /// <summary>
        /// 附件URL
        /// </summary>
        public string FileUrl { get; set; }

        /// <summary>
        /// 附件名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 是否显示在试题练习中
        /// </summary>
        public bool IsShow { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        /// 是否显示上传文本按钮
        /// </summary>
        public bool IsShowTextButton { get; set; }

        /// <summary>
        /// 是否显示上传图片按钮
        /// </summary>
        public bool IsShowImageButton { get; set; }

        /// <summary>
        /// 是否显示上传附件按钮
        /// </summary>
        public bool IsShowLinkButton { get; set; }


        public virtual QuestionCategory QuestionCategory { get; set; }

        public virtual ICollection<QuestionAnswer> QuestionAnswers { get; set; }

        public virtual ICollection<QuestionAttachment> QuestionAttachments { get; set; }

        //public virtual TrainPlatform TrainPlatform { get; set; }



        protected Question()
        {
        }

        public Question(
            Guid id,
            Guid? tenantId,
            Guid questionCategoryId,
            Guid? trainPlatformId,
            QuestionType type,
            string name,
            string content,
            string optionContent,
            int? optionSize,
            string optionA,
            string optionB,
            string optionC,
            string optionD,
            string optionE,
            string answer,
            QuestionDegree degree,
            string analysis,
            string source,
            string helpMessage,
            string helpVideo,
            string fileUrl,
            string fileName,
            bool isShow,
            bool enable,
            bool isShowTextButton,
            bool isShowImageButton,
            bool isShowLinkButton
        ) : base(id)
        {
            TenantId = tenantId;
            QuestionCategoryId = questionCategoryId;
            TrainPlatformId = trainPlatformId;
            Type = type;
            Name = name;
            Content = content;
            OptionContent = optionContent;
            OptionSize = optionSize;
            OptionA = optionA;
            OptionB = optionB;
            OptionC = optionC;
            OptionD = optionD;
            OptionE = optionE;
            Answer = answer;
            Degree = degree;
            Analysis = analysis;
            Source = source;
            HelpMessage = helpMessage;
            HelpVideo = helpVideo;
            FileUrl = fileUrl;
            FileName = fileName;
            IsShow = isShow;
            Enable = enable;
            IsShowTextButton = isShowTextButton;
            IsShowImageButton = isShowImageButton;
            IsShowLinkButton = isShowLinkButton;
        }
    }
}

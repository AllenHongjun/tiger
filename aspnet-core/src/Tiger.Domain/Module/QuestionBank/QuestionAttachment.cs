using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tiger.Module.QuestionBank
{
    /// <summary>
    /// 题目附件表
    /// </summary>
    public class QuestionAttachment:FullAuditedEntity<Guid>
    {
        /// <summary>
        /// 试题题目Id
        /// </summary>
        public Guid QuestionId { get; set; }

        /// <summary>
        /// 附件类型：1.内容，2.照片，3.文档，4.本地附件，5.本地视频，6.添加链接
        /// </summary>
        public QuestionAttachmentType AttachmentType { get; set; }

        /// <summary>
        /// 附件名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 附件内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sorting { get; set; }


        public virtual Question Question { get; set; }

        protected QuestionAttachment()
        {
        }

        public QuestionAttachment(
            Guid id,
            Guid questionId,
            QuestionAttachmentType attachmentType,
            string name,
            string content,
            int sorting
        ) : base(id)
        {
            QuestionId = questionId;
            AttachmentType = attachmentType;
            Name = name;
            Content = content;
            Sorting = sorting;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Module.TestQuestions
{
    /// <summary>
    /// 题目附件类型
    /// </summary>
    public enum QuestionAttachmentType
    {
        /// <summary>
        /// 内容
        /// </summary>
        Content = 1,

        /// <summary>
        /// 图片
        /// </summary>
        Photos = 2,

        /// <summary>
        /// 文档
        /// </summary>
        Documents = 3,

        /// <summary>
        /// 本地附件
        /// </summary>
        Attachments = 4,

        /// <summary>
        /// 视频
        /// </summary>
        Videos= 5,

        /// <summary>
        /// 链接
        /// </summary>
        Links = 6
    }
}

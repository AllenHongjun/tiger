using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Validation;

namespace Tiger.Module.QuestionBank.Dtos;

[Serializable]
public class CreateUpdateQuestionAttachmentDto
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
    [Required]
    [DynamicStringLength(typeof(QuestionAttachmentConsts), nameof(QuestionAttachmentConsts.MaxNameLength))]
    public string Name { get; set; }

    /// <summary>
    /// 附件内容
    /// </summary>
    [Required]
    [DynamicStringLength(typeof(QuestionAttachmentConsts), nameof(QuestionAttachmentConsts.MaxContextLength))]
    public string Content { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int Sorting { get; set; }
}
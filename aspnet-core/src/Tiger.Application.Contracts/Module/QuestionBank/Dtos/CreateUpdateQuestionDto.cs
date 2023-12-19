using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Validation;

namespace Tiger.Module.QuestionBank.Dtos;

[Serializable]
public class CreateUpdateQuestionDto
{
    /// <summary>
    /// 题库
    /// </summary>
    public Guid QuestionCategoryId { get; set; }

    /// <summary>
    /// 实训链接
    /// </summary>
    public Guid? PracticalTrainingId { get; set; }

    /// <summary>
    /// 类型 1.判断 2.单选 3.多选 4.填空 5.计算题 6.问答题 7.B型题,8.简答题 9.实训任务
    /// </summary>
    public QuestionType Type { get; set; }

    /// <summary>
    /// 题目名称
    /// </summary>
    [DynamicStringLength(typeof(QuestionConsts), nameof(QuestionConsts.MaxNameLength))]
    public string Name { get; set; }

    /// <summary>
    /// 题目内容
    /// </summary>
    [Required]
    public string Content { get; set; }

    /// <summary>
    /// 选项内容（用|分隔）
    /// </summary>
    public string OptionContent { get; set; }

    /// <summary>
    /// 选项数量
    /// </summary>
    public int? OptionSize { get; set; }

    /// <summary>
    /// 答案
    /// </summary>
    public string Answer { get; set; }

    /// <summary>
    /// 资源路径 例如: /path/to/myfile.html
    /// </summary>
    [DynamicStringLength(typeof(QuestionConsts), nameof(QuestionConsts.MaxTrainPlatformPath))]
    public string TrainPlatformPath { get; set; }

    /// <summary>
    /// 分数
    /// </summary>
    public decimal? Score { get; set; }

    /// <summary>
    /// 难易度：1.简单 2.普通 3.困难
    /// </summary>
    public QuestionDegree Degree { get; set; }

    /// <summary>
    /// 试题解析
    /// </summary>
    [DynamicStringLength(typeof(QuestionConsts), nameof(QuestionConsts.MaxAnalysisLength))]
    public string Analysis { get; set; }

    /// <summary>
    /// 出处
    /// </summary>
    [DynamicStringLength(typeof(QuestionConsts), nameof(QuestionConsts.MaxSourceLength))]
    public string Source { get; set; }

    /// <summary>
    /// 帮助文本
    /// </summary>
    [DynamicStringLength(typeof(QuestionConsts), nameof(QuestionConsts.MaxHelpMessageLength))]
    public string HelpMessage { get; set; }

    /// <summary>
    /// 帮助视频
    /// </summary>
    [DynamicStringLength(typeof(QuestionConsts), nameof(QuestionConsts.MaxHelpVideoLength))]
    public string HelpVideo { get; set; }

    /// <summary>
    /// 附件URL
    /// </summary>
    [DynamicStringLength(typeof(QuestionConsts), nameof(QuestionConsts.MaxFileUrlLength))]
    public string FileUrl { get; set; }

    /// <summary>
    /// 附件名称
    /// </summary>
    [DynamicStringLength(typeof(QuestionConsts), nameof(QuestionConsts.MaxFileNameLength))]
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
}
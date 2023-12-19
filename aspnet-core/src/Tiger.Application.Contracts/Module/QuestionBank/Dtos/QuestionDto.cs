using System;
using Volo.Abp.Application.Dtos;

namespace Tiger.Module.QuestionBank.Dtos;

/// <summary>
/// 题目表
/// </summary>
[Serializable]
public class QuestionDto : FullAuditedEntityDto<Guid>
{
    /// <summary>
    /// 题库
    /// </summary>
    public Guid QuestionCategoryId { get; set; }

    public string QuestionCateogryName { get; set; }

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
    public string Name { get; set; }

    /// <summary>
    /// 题目内容
    /// </summary>
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
    /// 每题分数
    /// </summary>
    public decimal Score { get; set; }

    /// <summary>
    /// 答案 判断题答案：正确/错误；多个填空之间的答案使用竖线 |分隔，如果一个填空有多个答案请用 & 开隔;
    /// </summary>
    public string Answer { get; set; }

    /// <summary>
    /// 资源路径 例如: /path/to/myfile.html
    /// </summary>
    public string TrainPlatformPath { get; set; }

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
}
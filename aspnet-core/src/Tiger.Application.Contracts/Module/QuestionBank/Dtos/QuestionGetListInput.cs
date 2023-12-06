using System;
using System.ComponentModel;
using Volo.Abp.Application.Dtos;

namespace Tiger.Module.QuestionBank.Dtos;

[Serializable]
public class QuestionGetListInput : PagedAndSortedResultRequestDto
{
    /// <summary>
    /// 其他过滤条件
    /// </summary>
    public string Filter { get; set; }

    /// <summary>
    /// 考试Id
    /// </summary>
    public Guid? ExamId { get; set; }

    /// <summary>
    /// 题库
    /// </summary>
    public Guid? QuestionCategoryId { get; set; }

    /// <summary>
    /// 创建开始时间
    /// </summary>
    public DateTime? CreateStartTime { get; set; }

    /// <summary>
    /// 创建结束时间
    /// </summary>
    public DateTime? CreateEndTime { get; set; }

    /// <summary>
    /// 实训链接
    /// </summary>
    public Guid? PracticalTrainingId { get; set; }

    /// <summary>
    /// 类型 1.判断 2.单选 3.多选 4.填空 5.计算题 6.问答题 7.B型题,8.简答题 9.实训任务
    /// </summary>
    public QuestionType? Type { get; set; }

    /// <summary>
    /// 难易度：1.简单 2.普通 3.困难
    /// </summary>
    public QuestionDegree? Degree { get; set; }

    /// <summary>
    /// 试题解析
    /// </summary>
    public string? Analysis { get; set; }

    /// <summary>
    /// 是否显示在试题练习中
    /// </summary>
    public bool? IsShow { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool? Enable { get; set; }
    
}
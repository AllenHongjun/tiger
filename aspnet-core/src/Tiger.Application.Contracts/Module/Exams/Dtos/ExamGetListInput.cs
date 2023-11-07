using System;
using System.ComponentModel;
using Volo.Abp.Application.Dtos;

namespace Tiger.Module.Exams.Dtos;

[Serializable]
public class ExamGetListInput : PagedAndSortedResultRequestDto
{
    /// <summary>
    /// 课程Id
    /// </summary>
    public Guid? CourseId { get; set; }

    /// <summary>
    /// 考试的试卷（母卷）
    /// </summary>
    public Guid? TestPaperId { get; set; }

    /// <summary>
    /// 题目分类
    /// </summary>
    public Guid? QuestionCategoryId { get; set; }

    /// <summary>
    /// 考试名称
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 封面
    /// </summary>
    public string? CoverUrl { get; set; }

    /// <summary>
    /// 编号
    /// </summary>
    public string? Number { get; set; }

    /// <summary>
    /// 类型：1.考试 2.练习 , 3 比赛
    /// </summary>
    public ExamType? ExamType { get; set; }

    /// <summary>
    /// 考试时间
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// 考试时长 单位：分钟
    /// </summary>
    public int? ExamDuration { get; set; }

    /// <summary>
    /// 是否每个人都不同
    /// </summary>
    public bool? IsDifferent { get; set; }

    /// <summary>
    /// 顺序不同（试卷相同时）
    /// </summary>
    public bool? IsDifferentOrder { get; set; }

    /// <summary>
    /// 提交后是否显示成绩
    /// </summary>
    public bool? IsShowScore { get; set; }

    /// <summary>
    /// 是否可以查看错题
    /// </summary>
    public bool? IsShowError { get; set; }

    /// <summary>
    /// 启用状态
    /// </summary>
    public bool? IsEnable { get; set; }

    /// <summary>
    /// 是否随到随考
    /// </summary>
    public bool? IsExamAnyTime { get; set; }

    /// <summary>
    /// 提示切屏次数
    /// </summary>
    public bool? IsShowWindowOnblur { get; set; }

    /// <summary>
    /// 考试最大次数
    /// </summary>
    public int? MaxExamCount { get; set; }

    /// <summary>
    /// 顺序
    /// </summary>
    public int? Sorting { get; set; }

    /// <summary>
    /// 仅考试当天可见
    /// </summary>
    public bool? OnlyExamDayVisible { get; set; }

    /// <summary>
    /// 是否启动自动实操评分
    /// </summary>
    public bool? IsStartSync { get; set; }

    /// <summary>
    /// 是否显示帮助内容
    /// </summary>
    public bool? IsShowHelp { get; set; }

    /// <summary>
    /// 是否中场休息
    /// </summary>
    public bool? HalftimeFlag { get; set; }

    /// <summary>
    /// 中场休息开始时间
    /// </summary>
    public DateTime? HalftimeStart { get; set; }

    /// <summary>
    /// 中场休息结束时间
    /// </summary>
    public DateTime? HalftimeEnd { get; set; }

    /// <summary>
    /// 扣款金额
    /// </summary>
    public decimal? DeductionAmounnt { get; set; }

    /// <summary>
    /// 扣款间隔（单位: 分钟）
    /// </summary>
    public int? DeductionInterval { get; set; }

    /// <summary>
    /// 比赛间隔（单位: 分钟）
    /// </summary>
    public int? Interval { get; set; }
}
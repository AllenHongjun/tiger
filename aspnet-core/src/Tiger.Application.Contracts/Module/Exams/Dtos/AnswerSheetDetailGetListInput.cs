using System;
using System.ComponentModel;
using Volo.Abp.Application.Dtos;

namespace Tiger.Module.Exams.Dtos;

[Serializable]
public class AnswerSheetDetailGetListInput : PagedAndSortedResultRequestDto
{
    /// <summary>
    /// 答卷Id
    /// </summary>
    public Guid? AnswerSheetId { get; set; }

    /// <summary>
    /// 试卷Id
    /// </summary>
    public Guid? TestPaperId { get; set; }

    /// <summary>
    /// 试卷详情Id
    /// </summary>
    public Guid? TestPaperDetailId { get; set; }

    /// <summary>
    /// 试题Id
    /// </summary>
    public Guid? QuestionId { get; set; }

    /// <summary>
    /// 答案
    /// </summary>
    public string? Answer { get; set; }

    /// <summary>
    /// 客观题评分
    /// </summary>
    public decimal? ObjectiveScore { get; set; }

    /// <summary>
    /// 实操题自动评分分数
    /// </summary>
    public decimal? OperateAutoScore { get; set; }

    /// <summary>
    /// 实操题人工评分分数
    /// </summary>
    public decimal? OperateManualScore { get; set; }

    /// <summary>
    /// 实操Id
    /// </summary>
    public string? OperateId { get; set; }

    /// <summary>
    /// 上次同步时间
    /// </summary>
    public DateTime? SyncTime { get; set; }

    /// <summary>
    /// 上次同步结果
    /// </summary>
    public string? SyncMessage { get; set; }
}
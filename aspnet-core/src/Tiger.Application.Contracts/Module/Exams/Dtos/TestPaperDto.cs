using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Tiger.Module.Exams.Dtos;

/// <summary>
/// 试卷
/// </summary>
[Serializable]
public class TestPaperDto : FullAuditedEntityDto<Guid>
{
    /// <summary>
    /// 主试卷、固定题目时0，随机题目或打乱顺序时录入主试卷的ID
    /// </summary>
    public Guid? TestPaperMainId { get; set; }

    /// <summary>
    /// 编号
    /// </summary>
    public string Number { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 类型 1.固定题目（手动或自动选题） 2.随机题目（根据策略每个学员的题目都不同） 3.固定题目打乱显示
    /// </summary>
    public TestPaperType Type { get; set; }

    /// <summary>
    /// 是否已组卷        固定题目需要组卷，随机选题不需要
    /// </summary>
    public bool IsComposing { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public bool Enable { get; set; }

    /// <summary>
    /// 是否包含全校老师
    /// </summary>
    public bool IsIncludeAllSchoolTeachers { get; set; }

    /// <summary>
    /// 是否限制评卷时间
    /// </summary>
    public bool IsLimitJudgeTime { get; set; }

    /// <summary>
    /// 评卷开始时间
    /// </summary>
    public DateTime? JudgeStartTime { get; set; }

    /// <summary>
    /// 评卷结束时间
    /// </summary>
    public DateTime? JudgeEndTime { get; set; }

    /// <summary>
    /// 试卷大题
    /// </summary>
    public List<TestPaperSectionDto> TestPaperSections { get; set; }

    /// <summary>
    /// 随机大题抽题规则
    /// </summary>
    public List<TestPaperStrategyDto> TestPaperStrategies { get; set; }
}
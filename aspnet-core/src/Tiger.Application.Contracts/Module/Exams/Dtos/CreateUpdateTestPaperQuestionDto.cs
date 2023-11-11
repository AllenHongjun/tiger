using System;
using Tiger.Module.QuestionBank;

namespace Tiger.Module.Exams.Dtos;

[Serializable]
public class CreateUpdateTestPaperQuestionDto
{
    /// <summary>
    /// 试卷ID
    /// </summary>
    public Guid TestPaperId { get; set; }

    /// <summary>
    /// 题目分类
    /// </summary>
    public Guid QuestionCategoryId { get; set; }

    /// <summary>
    /// 试题ID
    /// </summary>
    public Guid QuestionId { get; set; }

    /// <summary>
    /// 选题方式 1.自主选题 2.随机生成
    /// </summary>
    public TestPaperType TestPaperType { get; set; }

    /// <summary>
    /// 难易度：1.简单 2.普通 3.困难
    /// </summary>
    public QuestionDegree QuestionDegree { get; set; }

    /// <summary>
    /// 顺序
    /// </summary>
    public int Sorting { get; set; }

    /// <summary>
    /// 每题分数
    /// </summary>
    public decimal Score { get; set; }

    /// <summary>
    /// 漏选按错误处理
    /// </summary>
    public bool MissOptionInvalid { get; set; }
}
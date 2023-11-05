using System;
using System.ComponentModel;
using Volo.Abp.Application.Dtos;

namespace Tiger.Module.QuestionBank.Dtos;

[Serializable]
public class QuestionAnswerGetListInput : PagedAndSortedResultRequestDto
{
    /// <summary>
    /// 题目Id
    /// </summary>
    public Guid? QuestionId { get; set; }

    /// <summary>
    /// 题目答案                <remarks>        如果一个填空有多个答案请用 & 开隔 ,填空题内容：三个连续下划线___ (提示：在英文输入法下，按Shift键+减号键可敲出下划线)        </remarks>
    /// </summary>
    public string? Answer { get; set; }
}
using System;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Module.QuestionBank;

/// <summary>
/// 题目附件表
/// </summary>
public interface IQuestionAttachmentRepository : IRepository<QuestionAttachment, Guid>
{
}

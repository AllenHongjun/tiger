using System;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Module.QuestionBank;

/// <summary>
/// 实训平台
/// </summary>
public interface ITrainPlatformRepository : IRepository<TrainPlatform, Guid>
{
}

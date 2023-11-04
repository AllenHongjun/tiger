using System;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Module.Schools;

/// <summary>
/// 班级
/// </summary>
public interface IClassInfoRepository : IRepository<ClassInfo, Guid>
{
}

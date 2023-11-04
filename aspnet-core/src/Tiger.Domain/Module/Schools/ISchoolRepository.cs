using System;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Module.Schools;

/// <summary>
/// 学校管理
/// </summary>
public interface ISchoolRepository : IRepository<School, Guid>
{
}

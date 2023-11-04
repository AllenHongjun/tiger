using System;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Module.Teachings;

/// <summary>
/// 课程
/// </summary>
public interface ICourseRepository : IRepository<Course, Guid>
{
}

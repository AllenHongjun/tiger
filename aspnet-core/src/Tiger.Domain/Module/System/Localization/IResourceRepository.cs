using System;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Module.System.Localization;

public interface IResourceRepository : IRepository<Resource, Guid>
{
}

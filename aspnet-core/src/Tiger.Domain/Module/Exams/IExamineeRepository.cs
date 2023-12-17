using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace Tiger.Module.Exams;

/// <summary>
/// 考试人员表(应试人；参加考试者)
/// </summary>
public interface IExamineeRepository : IRepository<Examinee, Guid>
{
    Task<long> GetCountAsync(bool inExamineeTable, Guid? examId, Guid? organizationUnitId, DateTime? minCreationTime, DateTime? maxCreationTime, string filter = null, CancellationToken cancellationToken = default);
    Task<List<IdentityUser>> GetListAsync(bool inExamineeTable, Guid? examId, Guid? organizationUnitId, DateTime? minCreationTime, DateTime? maxCreationTime, string filter = null, string sorting = null, int maxResultCount = 50, int skipCount = 0, CancellationToken cancellationToken = default);
    Task<List<Examinee>> GetListByIdsAsync(Guid? exmId, List<Guid> userIds, bool includeDetails = false, CancellationToken cancellationToken = default);
}

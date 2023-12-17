using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity;

namespace Tiger.Module.Exams;

public class ExamineeRepository : EfCoreRepository<TigerDbContext, Examinee, Guid>, IExamineeRepository
{
    public ExamineeRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    protected  IQueryable<IdentityUser> GetQuery(
        bool inExamineeTable,
        Guid? examId,
        Guid? organizationUnitId,
        DateTime? minCreationTime,
        DateTime? maxCreationTime,
        string filter = null)
    {
        var queryUser = DbContext.Set<IdentityUser>()
                .WhereIf(organizationUnitId.HasValue, x => x.OrganizationUnits.Any(og => og.OrganizationUnitId == organizationUnitId))
                .WhereIf(minCreationTime.HasValue, x => x.CreationTime >= minCreationTime)
                .WhereIf(maxCreationTime.HasValue, x => x.CreationTime <= maxCreationTime)
                .WhereIf(!filter.IsNullOrEmpty(), x => x.UserName.Contains(filter) || x.PhoneNumber.Contains(filter) || x.Email.Contains(filter) || x.Surname.Contains(filter) || x.Name.Contains(filter));

        var queryExaminee = DbContext.Set<Examinee>()
            .WhereIf(examId.HasValue, x => x.ExamId == examId);
        IQueryable<IdentityUser> query;
        if (inExamineeTable)
        {
            // 已选择考生列表
             query = from examinee in queryExaminee
                        join user in queryUser on examinee.UserId equals user.Id
                        select user;
        }
        else
        {
            // 待选择考生列表 LINQ左外部联接： https://learn.microsoft.com/zh-cn/dotnet/csharp/linq/perform-left-outer-joins
            query = from user in queryUser
                         join examinee in queryExaminee on user.Id equals examinee.UserId into userExaminees
                         from ue in userExaminees.DefaultIfEmpty()
                         where ue.Id == null
                         select user;
        }
        return query;
    }


    public async Task<long> GetCountAsync(
        bool inExamineeTable,
        Guid? examId,
        Guid? organizationUnitId,
        DateTime? minCreationTime,
        DateTime? maxCreationTime,
        string filter = null, CancellationToken cancellationToken = default)
    {
        var query =  GetQuery(inExamineeTable, examId, organizationUnitId, minCreationTime, maxCreationTime, filter);
        return await query.LongCountAsync(GetCancellationToken(cancellationToken));
    }

    public async Task<List<IdentityUser>> GetListAsync(
        bool inExamineeTable,
        Guid? examId,
        Guid? organizationUnitId,
        DateTime? minCreationTime,
        DateTime? maxCreationTime,
        string filter = null,
        string sorting = null, int maxResultCount = 50, int skipCount = 0,
         CancellationToken cancellationToken = default)
    {
        var query =  GetQuery(inExamineeTable, examId, organizationUnitId, minCreationTime, maxCreationTime, filter);
        return  await query
                    .OrderBy(sorting.IsNullOrEmpty() ? nameof(IdentityUser.CreationTime) : sorting)
                    .PageBy(skipCount, maxResultCount)
                    .ToListAsync(GetCancellationToken(cancellationToken));
    }


    public override IQueryable<Examinee> WithDetails()
    {
        return  GetQueryable().IncludeDetails();
    }
}
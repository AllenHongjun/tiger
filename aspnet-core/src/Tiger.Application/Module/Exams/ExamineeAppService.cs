using System;
using System.Linq;
using System.Threading.Tasks;
using Tiger.Permissions;
using Tiger.Module.Exams.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Identity;
using System.Diagnostics;
using Volo.Abp.Users;
using Microsoft.AspNetCore.Authorization;
using Tiger.Infrastructure.BackgroundTasks.Abstractions.Enum;
using Tiger.Module.TaskManagement.Dtos;
using Tiger.Module.TaskManagement.Permissions;

namespace Tiger.Module.Exams;


/// <summary>
/// 考试人员表(应试人；参加考试者)
/// </summary>
public class ExamineeAppService : CrudAppService<Examinee, ExamineeDto, Guid, ExamineeGetListInput, CreateUpdateExamineeDto, CreateUpdateExamineeDto>,
    IExamineeAppService
{
    

    private readonly IExamineeRepository _repository;
    private readonly IIdentityUserRepository _identityUserRepository;
    private readonly IdentityUserManager _identityUserManager;


    public ExamineeAppService(IExamineeRepository repository, IIdentityUserRepository identityUserRepository) : base(repository)
    {
        _repository = repository;
        _identityUserRepository=identityUserRepository;
    }

    protected override IQueryable<Examinee> CreateFilteredQuery(ExamineeGetListInput input)
    {
        // TODO: AbpHelper generated
        return  base.CreateFilteredQuery(input)
            .WhereIf(input.ExamId != null, x => x.ExamId == input.ExamId)
            .WhereIf(input.UserId != null, x => x.UserId == input.UserId)
            .WhereIf(!input.UserName.IsNullOrWhiteSpace(), x => x.UserName.Contains(input.UserName))
            .WhereIf(!input.FullName.IsNullOrWhiteSpace(), x => x.FullName.Contains(input.FullName))
            .WhereIf(!input.Email.IsNullOrWhiteSpace(), x => x.Email.Contains(input.Email))
            .WhereIf(!input.PhoneNumber.IsNullOrWhiteSpace(), x => x.PhoneNumber.Contains(input.PhoneNumber))
            .WhereIf(input.OrganizationUnitId != null, x => x.OrganizationUnitId == input.OrganizationUnitId)
            .WhereIf(!input.OrganizationUnitName.IsNullOrWhiteSpace(), x => x.OrganizationUnitName.Contains(input.OrganizationUnitName))
            ;
    }

    public override async Task<PagedResultDto<ExamineeDto>> GetListAsync(ExamineeGetListInput input)
    {
        var count = await _repository.GetCountAsync(input.inExamineeTable, input.ExamId,input.OrganizationUnitId,input.minCreationTime, input.maxCreationTime, input.filter);

        var list = await _repository.GetListAsync(input.inExamineeTable, input.ExamId, input.OrganizationUnitId, input.minCreationTime, input.maxCreationTime, input.filter,
             input.Sorting, input.MaxResultCount, input.SkipCount);
        var examinees = ObjectMapper.Map<List<IdentityUser>, List<ExamineeDto>>(list);

        foreach (var item in examinees)
        {
            List<OrganizationUnit> organizationUnits = await _identityUserRepository.GetOrganizationUnitsAsync(item.UserId);
            item.OrganizationUnitName = string.Join("/", organizationUnits.Select(x => x.DisplayName).ToList());
        }
        return new PagedResultDto<ExamineeDto>(count, examinees );
    }

    public async virtual Task BulkCreateAsync(ExamineeBatchInputDto input)
    {
        if (!input.UserIds.Any())
        {
            return;
        }
        //_identityUserRepository.
        //var jobs = await GetListAsync(input);
        

        //await BackgroundJobManager.BulkQueueAsync(jobs);
    }

}

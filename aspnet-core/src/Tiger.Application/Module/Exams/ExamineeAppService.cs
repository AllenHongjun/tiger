using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tiger.Module.Exams.Dtos;
using Tiger.Volo.Abp.Identity;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Identity;

namespace Tiger.Module.Exams;


/// <summary>
/// 考试人员表(应试人；参加考试者)
/// </summary>
[RemoteService(IsEnabled = false)]
public class ExamineeAppService : CrudAppService<Examinee, ExamineeDto, Guid, ExamineeGetListInput, CreateUpdateExamineeDto, CreateUpdateExamineeDto>,
    IExamineeAppService
{
    

    private readonly IExamineeRepository _repository;
    private readonly IdentityUserManager _identityUserManager;
    private readonly ITigerIdentityUserRepository _tigerIdentityUserRepository;


    public ExamineeAppService(
        IExamineeRepository repository,
        ITigerIdentityUserRepository tigerIdentityUserRepository) : base(repository)
    {
        _repository = repository;
        _tigerIdentityUserRepository=tigerIdentityUserRepository;
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
            List<OrganizationUnit> organizationUnits = await _tigerIdentityUserRepository.GetOrganizationUnitsAsync(item.UserId);
            item.OrganizationUnitName = string.Join("/", organizationUnits.Select(x => x.DisplayName).ToList());
        }
        return new PagedResultDto<ExamineeDto>(count, examinees );
    }

    public async virtual Task BulkCreateAsync(ExamineeBatchInputDto input)
    {
        if (!input.UserIds.Any() && !input.OrganizationUnitIds.Any())
        {
            return;
        }
        
        List<IdentityUser> users;

        if (input.UserIds.Any())
        {
            users = await _tigerIdentityUserRepository.GetListByIdsAsync(input.UserIds);
        }
        else
        {
            users = await _tigerIdentityUserRepository.GetUsersInOrganizationsListAsync(input.OrganizationUnitIds);
        }
        
        foreach (var user in users)
        {
            var examinee = new Examinee(GuidGenerator.Create(), CurrentTenant.Id, input.ExamId, user.Id, user.UserName, user.Surname + user.Name,
                user.Email, user.PhoneNumber);
            await _repository.InsertAsync(examinee);
        }
        await CurrentUnitOfWork.SaveChangesAsync();

    }

    public async virtual Task BulkDeleteAsync(ExamineeBatchInputDto input)
    {
        if (!input.UserIds.Any())
        {
            return;
        }
        var examinees = await _repository.GetListByIdsAsync(input.ExamId, input.UserIds);
        foreach (var examinee in examinees)
        {
            
            await _repository.DeleteAsync(examinee);
        }
        await CurrentUnitOfWork.SaveChangesAsync();
    }

}

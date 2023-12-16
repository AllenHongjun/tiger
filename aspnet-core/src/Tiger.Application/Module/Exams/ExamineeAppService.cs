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

namespace Tiger.Module.Exams;


/// <summary>
/// 考试人员表(应试人；参加考试者)
/// </summary>
public class ExamineeAppService : CrudAppService<Examinee, ExamineeDto, Guid, ExamineeGetListInput, CreateUpdateExamineeDto, CreateUpdateExamineeDto>,
    IExamineeAppService
{
    protected override string GetPolicyName { get; set; } = TigerPermissions.Examinee.Default;
    protected override string GetListPolicyName { get; set; } = TigerPermissions.Examinee.Default;
    protected override string CreatePolicyName { get; set; } = TigerPermissions.Examinee.Create;
    protected override string UpdatePolicyName { get; set; } = TigerPermissions.Examinee.Update;
    protected override string DeletePolicyName { get; set; } = TigerPermissions.Examinee.Delete;

    private readonly IExamineeRepository _repository;

    public ExamineeAppService(IExamineeRepository repository) : base(repository)
    {
        _repository = repository;
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

    /*
     * 查询一场考试的 考生列表数据
     1. 根据考试Id查询 考试管理的考生Id
     2. 查找在这些考生id中存在的用户信息
     3. 如果用in sql server 4000 字符长度限制 

    1. exisit 关键字
    2. join关联查询 使用 inner join 
    3. 将一个list分裂为多个符合长度的list
    4. 转化为sql 表值参数 使用sql语句查询
     */


    public override async Task<PagedResultDto<ExamineeDto>> GetListAsync(ExamineeGetListInput input)
    {
        var count = await _repository.GetCountAsync(input.inExamineeTable, input.ExamId,input.OrganizationUnitId,input.minCreationTime, input.maxCreationTime, input.filter);

        var list = await _repository.GetListAsync(input.inExamineeTable, input.ExamId, input.OrganizationUnitId, input.minCreationTime, input.maxCreationTime, input.filter,
             input.Sorting, input.SkipCount, input.MaxResultCount);

        return new PagedResultDto<ExamineeDto>(count,
            ObjectMapper.Map<List<IdentityUser>, List<ExamineeDto>>(list));
    }



}

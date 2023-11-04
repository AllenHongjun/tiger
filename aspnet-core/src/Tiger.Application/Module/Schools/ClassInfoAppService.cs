using System;
using System.Linq;
using System.Threading.Tasks;
using Tiger.Permissions;
using Tiger.Module.Schools.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Module.Schools;


/// <summary>
/// 班级
/// </summary>
public class ClassInfoAppService : CrudAppService<ClassInfo, ClassInfoDto, Guid, ClassInfoGetListInput, CreateUpdateClassInfoDto, CreateUpdateClassInfoDto>,
    IClassInfoAppService
{
    

    private readonly IClassInfoRepository _repository;

    public ClassInfoAppService(IClassInfoRepository repository) : base(repository)
    {
        _repository = repository;
    }

    protected override  IQueryable<ClassInfo> CreateFilteredQuery(ClassInfoGetListInput input)
    {
        // TODO: AbpHelper generated
        return ( base.CreateFilteredQuery(input))
            .WhereIf(!input.Name.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Name))
            .WhereIf(input.SchoolId != null, x => x.SchoolId == input.SchoolId)
            .WhereIf(input.IsEnable != null, x => x.IsEnable == input.IsEnable)
            ;
    }
}

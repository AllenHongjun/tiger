using System;
using System.Linq;
using System.Threading.Tasks;
using Tiger.Permissions;
using Tiger.Module.Teachings.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Module.Teachings;


/// <summary>
/// 课程
/// </summary>
public class CourseAppService : CrudAppService<Course, CourseDto, Guid, CourseGetListInput, CreateUpdateCourseDto, CreateUpdateCourseDto>,
    ICourseAppService
{
    protected override string GetPolicyName { get; set; } = TigerPermissions.Course.Default;
    protected override string GetListPolicyName { get; set; } = TigerPermissions.Course.Default;
    protected override string CreatePolicyName { get; set; } = TigerPermissions.Course.Create;
    protected override string UpdatePolicyName { get; set; } = TigerPermissions.Course.Update;
    protected override string DeletePolicyName { get; set; } = TigerPermissions.Course.Delete;

    private readonly ICourseRepository _repository;

    public CourseAppService(ICourseRepository repository) : base(repository)
    {
        _repository = repository;
    }

    protected override  IQueryable<Course> CreateFilteredQuery(CourseGetListInput input)
    {
        // TODO: AbpHelper generated
        return base.CreateFilteredQuery(input)
            .WhereIf(input.Type != null, x => x.Type == input.Type)
            .WhereIf(!input.Name.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Name))
            .WhereIf(!input.Description.IsNullOrWhiteSpace(), x => x.Description.Contains(input.Description))
            .WhereIf(!input.Cover.IsNullOrWhiteSpace(), x => x.Cover.Contains(input.Cover))
            .WhereIf(input.Enable != null, x => x.Enable == input.Enable)
            .WhereIf(input.Sorting != null, x => x.Sorting == input.Sorting)
            ;
    }
}

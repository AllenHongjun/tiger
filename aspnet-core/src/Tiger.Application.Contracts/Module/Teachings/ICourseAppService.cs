using System;
using Tiger.Module.Teachings.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Module.Teachings;


/// <summary>
/// 课程
/// </summary>
public interface ICourseAppService :
    ICrudAppService< 
                CourseDto, 
        Guid, 
        CourseGetListInput,
        CreateUpdateCourseDto,
        CreateUpdateCourseDto>
{

}
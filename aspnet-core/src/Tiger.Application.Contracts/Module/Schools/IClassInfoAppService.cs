using System;
using Tiger.Module.Schools.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Module.Schools;


/// <summary>
/// 班级
/// </summary>
public interface IClassInfoAppService :
    ICrudAppService< 
                ClassInfoDto, 
        Guid, 
        ClassInfoGetListInput,
        CreateUpdateClassInfoDto,
        CreateUpdateClassInfoDto>
{

}
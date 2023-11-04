using System;
using Tiger.Module.Schools.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Module.Schools;


/// <summary>
/// 学校管理
/// </summary>
public interface ISchoolAppService :
    ICrudAppService< 
                SchoolDto, 
        Guid, 
        SchoolGetListInput,
        CreateUpdateSchoolDto,
        CreateUpdateSchoolDto>
{

}
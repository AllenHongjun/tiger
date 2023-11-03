using System;
using Tiger.Module.System.Area.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Module.System.Area;

public interface IRegionAppService :
    ICrudAppService< 
        RegionDto, 
        int, 
        RegionGetListInput,
        CreateUpdateRegionDto,
        CreateUpdateRegionDto>
{

    ListResultDto<RegionDto> GetListByParentCode(long parentCode);

    ListResultDto<RegionDto> GetAllList();
}
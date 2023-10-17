using System;
using Tiger.Module.System.Area.Dtos;
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

}
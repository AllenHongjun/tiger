using System;
using Tiger.Basic.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Basic
{
    public interface ISupplyAppService :
        ICrudAppService< 
            SupplyDto, 
            Guid, 
            PagedAndSortedResultRequestDto,
            CreateUpdateSupplyDto,
            CreateUpdateSupplyDto>
    {

    }
}
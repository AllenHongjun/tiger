using System;
using Tiger.Basic.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Basic
{
    public interface ISkuAppService :
        ICrudAppService< 
            SkuDto, 
            Guid, 
            PagedAndSortedResultRequestDto,
            CreateUpdateSkuDto,
            CreateUpdateSkuDto>
    {

    }
}
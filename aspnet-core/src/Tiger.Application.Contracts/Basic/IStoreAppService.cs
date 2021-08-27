using System;
using Tiger.Basic.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Basic
{
    public interface IStoreAppService :
        ICrudAppService< 
            StoreDto, 
            Guid, 
            PagedAndSortedResultRequestDto,
            CreateUpdateStoreDto,
            CreateUpdateStoreDto>
    {

    }
}
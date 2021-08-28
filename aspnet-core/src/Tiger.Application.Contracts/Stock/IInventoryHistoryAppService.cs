using System;
using Tiger.Stock.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Stock
{
    public interface IInventoryHistoryAppService :
        ICrudAppService< 
            InventoryHistoryDto, 
            Guid, 
            PagedAndSortedResultRequestDto,
            CreateUpdateInventoryHistoryDto,
            CreateUpdateInventoryHistoryDto>
    {

    }
}
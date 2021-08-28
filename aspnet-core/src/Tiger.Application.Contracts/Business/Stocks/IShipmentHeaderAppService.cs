using System;
using Tiger.Business.Stocks.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Business.Stocks
{
    public interface IShipmentHeaderAppService :
        ICrudAppService< 
            ShipmentHeaderDto, 
            Guid, 
            PagedAndSortedResultRequestDto,
            CreateUpdateShipmentHeaderDto,
            CreateUpdateShipmentHeaderDto>
    {

    }
}
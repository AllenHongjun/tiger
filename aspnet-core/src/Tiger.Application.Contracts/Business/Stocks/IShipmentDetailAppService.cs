using System;
using Tiger.Business.Stocks.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Business.Stocks
{
    public interface IShipmentDetailAppService :
        ICrudAppService< 
            ShipmentDetailDto, 
            Guid, 
            PagedAndSortedResultRequestDto,
            CreateUpdateShipmentDetailDto,
            CreateUpdateShipmentDetailDto>
    {

    }
}
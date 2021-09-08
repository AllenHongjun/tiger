using System;
using Tiger.Business.Orders.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Business.Orders
{
    public interface IOrderReturnDetailAppService :
        ICrudAppService< 
            OrderReturnDetailDto, 
            Guid, 
            PagedAndSortedResultRequestDto,
            CreateUpdateOrderReturnDetailDto,
            CreateUpdateOrderReturnDetailDto>
    {

    }
}
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Orders.OrderItems
{
    public interface IOrderItemAppService:
        ICrudAppService<
            OrderItemDto, 
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateOrderItemDto> 
    {


    }
}

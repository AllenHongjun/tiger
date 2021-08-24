using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Orders.Orders
{
    public interface IOrderAppService:
        ICrudAppService<
            OrderDto,
            Guid,
            GetOrderListDto,
            CreateUpdateOrderDto,
            CreateUpdateOrderDto>
    {

    }
}

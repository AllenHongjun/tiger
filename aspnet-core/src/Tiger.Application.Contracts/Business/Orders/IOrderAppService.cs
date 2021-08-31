using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Business.Orders.Dtos;
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

        /// <summary>
        /// 生成订单
        /// </summary>
        /// <param name="createOrderDto"></param>
        /// <returns></returns>
        Task<OrderDto> CreateOrder(CreateOrderDto createOrderDto);
    }
}

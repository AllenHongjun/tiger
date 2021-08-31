using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Business.Orders.Dtos;
using Tiger.Orders.Orders;
using Volo.Abp;

namespace Tiger.Controllers.WebApp
{
    [RemoteService(Name = "Order")]
    [Area("WebApp")]
    [ControllerName("Order")]
    [Route("api/webapp/order")]
    [ApiExplorerSettings(GroupName = "api")]
    public class OrderController:TigerController
    {
        protected readonly IOrderAppService _orderAppService;

        public OrderController(IOrderAppService orderAppService)
        {
            _orderAppService = orderAppService;
        }


        /// <summary>
        /// 生成订单
        /// </summary>
        /// <param name="createOrderDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<OrderDto> CreateOrder(CreateOrderDto createOrderDto)
        {
            createOrderDto.memberId = MemberId;
            return await _orderAppService.CreateOrder(createOrderDto);
        }
    }
}

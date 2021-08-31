using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Orders.Orders;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Identity;

namespace Tiger.Controllers.Admin.Basics
{
    /// <summary>
    /// 产品分类
    /// </summary>

    [RemoteService(Name = "Order")]
    [Area("Orders")]
    [ControllerName("Order")]
    [Route("api/basic/order")]
    [ApiExplorerSettings(GroupName = "admin-basic")]
    public class OrderController : TigerController
    {
        protected readonly IOrderAppService _orderAppService;

        public OrderController(IOrderAppService orderAppService)
        {
            _orderAppService = orderAppService;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<OrderDto> CreateAsync(CreateUpdateOrderDto input)
        {
            
            return _orderAppService.CreateAsync(input);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public Task DeleteAsync(Guid id)
        {
            return _orderAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 获取单条
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public Task<OrderDto> GetAsync(Guid id)
        {
            return _orderAppService.GetAsync(id);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        //[Route("all")]
        public Task<PagedResultDto<OrderDto>> GetListAsync(GetOrderListDto input)
        {
            return _orderAppService.GetListAsync(input);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public Task<OrderDto> UpdateAsync(Guid id, CreateUpdateOrderDto input)
        {
            return _orderAppService.UpdateAsync(id, input);
        }

        
    }
}

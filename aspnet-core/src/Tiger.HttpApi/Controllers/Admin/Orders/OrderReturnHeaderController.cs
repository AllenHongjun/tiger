using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Tiger.Business.Members;
using Tiger.Business.Members.Dtos;
using Tiger.Business.Orders;
using Tiger.Business.Orders.Dtos;
using Tiger.Business.Stocks;
using Tiger.Business.Stocks.Dtos;
using Tiger.Stock;
using Tiger.Stock.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Tiger.Controllers.Admin.OrderReturnHeaders
{
    /// <summary>
    /// 出库单
    /// </summary>

    [RemoteService(Name = "OrderReturnHeader")]
    [Area("Orders")]
    [ControllerName("OrderReturnHeader")]
    [Route("api/order/orderReturnHeader")]
    [ApiExplorerSettings(GroupName = "admin-erp")]
    public class OrderReturnHeaderController : TigerController
    {
        protected readonly IOrderReturnHeaderAppService _orderReturnHeaderAppService;

        public OrderReturnHeaderController(IOrderReturnHeaderAppService orderReturnHeaderAppService)
        {
            _orderReturnHeaderAppService = orderReturnHeaderAppService;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<OrderReturnHeaderDto> CreateAsync(CreateUpdateOrderReturnHeaderDto input)
        {
            return _orderReturnHeaderAppService.CreateAsync(input);
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
            return _orderReturnHeaderAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 获取单条
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public Task<OrderReturnHeaderDto> GetAsync(Guid id)
        {
            return _orderReturnHeaderAppService.GetAsync(id);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        public Task<PagedResultDto<OrderReturnHeaderDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return _orderReturnHeaderAppService.GetListAsync(input);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public Task<OrderReturnHeaderDto> UpdateAsync(Guid id, CreateUpdateOrderReturnHeaderDto input)
        {
            return _orderReturnHeaderAppService.UpdateAsync(id, input);
        }


        
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Books;
using Tiger.Orders.OrderSettings;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Identity;

namespace Tiger.Controllers.Admin.Basics
{
    /// <summary>
    /// 产品分类
    /// </summary>

    [RemoteService(Name = "OrderSetting")]
    [Area("OrderSettings")]
    [ControllerName("OrderSetting")]
    [Route("api/basic/orderSetting")]
    [ApiExplorerSettings(GroupName = "admin-basic")]
    public class OrderSettingController : TigerController, IOrderSettingAppService
    {
        protected readonly IOrderSettingAppService _orderSettingAppService;

        public OrderSettingController(IOrderSettingAppService orderSettingAppService)
        {
            _orderSettingAppService = orderSettingAppService;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<OrderSettingDto> CreateAsync(CreateUpdateOrderSettingDto input)
        {
            return _orderSettingAppService.CreateAsync(input);
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
            return _orderSettingAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 获取单条
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public Task<OrderSettingDto> GetAsync(Guid id)
        {
            return _orderSettingAppService.GetAsync(id);
        }

       

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        //[Route("all")]
        public Task<PagedResultDto<OrderSettingDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return _orderSettingAppService.GetListAsync(input);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public Task<OrderSettingDto> UpdateAsync(Guid id, CreateUpdateOrderSettingDto input)
        {
            return _orderSettingAppService.UpdateAsync(id, input);
        }

        
    }
}

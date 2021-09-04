using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Tiger.Stock;
using Tiger.Stock.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Tiger.Controllers.Admin.InventoryHistorys
{
    /// <summary>
    /// 实时库存
    /// </summary>

    [RemoteService(Name = "InventoryHistory")]
    [Area("Stocks")]
    [ControllerName("InventoryHistory")]
    [Route("api/stock/inventoryHistory")]
    [ApiExplorerSettings(GroupName = "admin-erp")]
    public class InventoryHistoryController : TigerController, IInventoryHistoryAppService
    {
        protected readonly IInventoryHistoryAppService _inventoryHistoryAppService;

        public InventoryHistoryController(IInventoryHistoryAppService inventoryHistoryAppService)
        {
            _inventoryHistoryAppService = inventoryHistoryAppService;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<InventoryHistoryDto> CreateAsync(CreateUpdateInventoryHistoryDto input)
        {
            return _inventoryHistoryAppService.CreateAsync(input);
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
            return _inventoryHistoryAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 获取单条
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public Task<InventoryHistoryDto> GetAsync(Guid id)
        {
            return _inventoryHistoryAppService.GetAsync(id);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        public Task<PagedResultDto<InventoryHistoryDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return _inventoryHistoryAppService.GetListAsync(input);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public Task<InventoryHistoryDto> UpdateAsync(Guid id, CreateUpdateInventoryHistoryDto input)
        {
            return _inventoryHistoryAppService.UpdateAsync(id, input);
        }


        
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Tiger.Stock;
using Tiger.Stock.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Tiger.Controllers.Admin.Inventorys
{
    /// <summary>
    /// 实时库存
    /// </summary>

    [RemoteService(Name = "Inventory")]
    [Area("Basics")]
    [ControllerName("Inventory")]
    [Route("api/basic/inventory")]
    [ApiExplorerSettings(GroupName = "admin-basic")]
    public class InventoryController : TigerController, IInventoryAppService
    {
        protected readonly IInventoryAppService _inventoryAppService;

        public InventoryController(IInventoryAppService inventoryAppService)
        {
            _inventoryAppService = inventoryAppService;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<InventoryDto> CreateAsync(CreateUpdateInventoryDto input)
        {
            return _inventoryAppService.CreateAsync(input);
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
            return _inventoryAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 获取单条
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public Task<InventoryDto> GetAsync(Guid id)
        {
            return _inventoryAppService.GetAsync(id);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        public Task<PagedResultDto<InventoryDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return _inventoryAppService.GetListAsync(input);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public Task<InventoryDto> UpdateAsync(Guid id, CreateUpdateInventoryDto input)
        {
            return _inventoryAppService.UpdateAsync(id, input);
        }


        
    }
}

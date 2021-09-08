using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiger.Basic;
using Tiger.Basic.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Tiger.Controllers.Admin.Basics
{
    /// <summary>
    /// 产品分类
    /// </summary>

    [RemoteService(Name = "Warehouse")]
    [Area("Basics")]
    [ControllerName("Warehouse")]
    [Route("api/basic/warehouse")]
    [ApiExplorerSettings(GroupName = "admin-basic")]
    public class WarehouseController : TigerController, IWarehouseAppService
    {
        protected readonly IWarehouseAppService _warehouseAppService;

        public WarehouseController(IWarehouseAppService warehouseAppService)
        {
            _warehouseAppService = warehouseAppService;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<WarehouseDto> CreateAsync(WarehouseCreateDto input)
        {
            return _warehouseAppService.CreateAsync(input);
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
            return _warehouseAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 获取单条
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public Task<WarehouseDto> GetAsync(Guid id)
        {
            return _warehouseAppService.GetAsync(id);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        //[Route("all")]
        public Task<PagedResultDto<WarehouseDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return _warehouseAppService.GetListAsync(input);
        }

        /// <summary>
        /// 获取所有仓库
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        public Task<List<WarehouseDto>> GetAllAsync()
        {
            return _warehouseAppService.GetAllAsync();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public Task<WarehouseDto> UpdateAsync(Guid id, WarehouseUpdateDto input)
        {
            return _warehouseAppService.UpdateAsync(id, input);
        }


        

        
    }
}

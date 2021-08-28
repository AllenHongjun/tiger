using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Tiger.Business.Members;
using Tiger.Business.Members.Dtos;
using Tiger.Business.Stocks;
using Tiger.Business.Stocks.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Tiger.Controllers.Admin.ShipmentHeaders
{
    /// <summary>
    /// 出库单
    /// </summary>

    [RemoteService(Name = "ShipmentHeader")]
    [Area("Basics")]
    [ControllerName("ShipmentHeader")]
    [Route("api/basic/shipmentHeader")]
    [ApiExplorerSettings(GroupName = "admin-erp")]
    public class ShipmentHeaderController : TigerController, IShipmentHeaderAppService
    {
        protected readonly IShipmentHeaderAppService _shipmentHeaderAppService;

        public ShipmentHeaderController(IShipmentHeaderAppService shipmentHeaderAppService)
        {
            _shipmentHeaderAppService = shipmentHeaderAppService;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<ShipmentHeaderDto> CreateAsync(CreateUpdateShipmentHeaderDto input)
        {
            return _shipmentHeaderAppService.CreateAsync(input);
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
            return _shipmentHeaderAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 获取单条
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public Task<ShipmentHeaderDto> GetAsync(Guid id)
        {
            return _shipmentHeaderAppService.GetAsync(id);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        public Task<PagedResultDto<ShipmentHeaderDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return _shipmentHeaderAppService.GetListAsync(input);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public Task<ShipmentHeaderDto> UpdateAsync(Guid id, CreateUpdateShipmentHeaderDto input)
        {
            return _shipmentHeaderAppService.UpdateAsync(id, input);
        }


        
    }
}

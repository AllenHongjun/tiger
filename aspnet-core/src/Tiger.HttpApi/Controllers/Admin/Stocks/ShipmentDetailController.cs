using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Tiger.Business.Stocks;
using Tiger.Business.Stocks.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Tiger.Controllers.Admin.ShipmentDetails
{
    /// <summary>
    /// 产品分类
    /// </summary>

    [RemoteService(Name = "ShipmentDetail")]
    [Area("Basics")]
    [ControllerName("ShipmentDetail")]
    [Route("api/basic/shipmentDetail")]
    [ApiExplorerSettings(GroupName = "admin-erp")]
    public class ShipmentDetailController : TigerController, IShipmentDetailAppService
    {
        protected readonly IShipmentDetailAppService _shipmentDetailAppService;

        public ShipmentDetailController(IShipmentDetailAppService shipmentDetailAppService)
        {
            _shipmentDetailAppService = shipmentDetailAppService;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<ShipmentDetailDto> CreateAsync(CreateUpdateShipmentDetailDto input)
        {
            return _shipmentDetailAppService.CreateAsync(input);
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
            return _shipmentDetailAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 获取单条
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public Task<ShipmentDetailDto> GetAsync(Guid id)
        {
            return _shipmentDetailAppService.GetAsync(id);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        public Task<PagedResultDto<ShipmentDetailDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return _shipmentDetailAppService.GetListAsync(input);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public Task<ShipmentDetailDto> UpdateAsync(Guid id, CreateUpdateShipmentDetailDto input)
        {
            return _shipmentDetailAppService.UpdateAsync(id, input);
        }


        
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Tiger.Business.Members;
using Tiger.Business.Members.Dtos;
using Tiger.Business.Purchases;
using Tiger.Business.Purchases.Dtos;
using Tiger.Business.Stocks;
using Tiger.Business.Stocks.Dtos;
using Tiger.Stock;
using Tiger.Stock.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Tiger.Controllers.Admin.PurchaseReturnHeaders
{
    /// <summary>
    /// 出库单
    /// </summary>

    [RemoteService(Name = "PurchaseReturnHeader")]
    [Area("Purchases")]
    [ControllerName("PurchaseReturnHeader")]
    [Route("api/purchase/purchaseReturnHeader")]
    [ApiExplorerSettings(GroupName = "admin-erp")]
    public class PurchaseReturnHeaderController : TigerController
    {
        protected readonly IPurchaseReturnHeaderAppService _purchaseReturnHeaderAppService;

        public PurchaseReturnHeaderController(IPurchaseReturnHeaderAppService purchaseReturnHeaderAppService)
        {
            _purchaseReturnHeaderAppService = purchaseReturnHeaderAppService;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<PurchaseReturnHeaderDto> CreateAsync(CreateUpdatePurchaseReturnHeaderDto input)
        {
            return _purchaseReturnHeaderAppService.CreateAsync(input);
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
            return _purchaseReturnHeaderAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 获取单条
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public Task<PurchaseReturnHeaderDto> GetAsync(Guid id)
        {
            return _purchaseReturnHeaderAppService.GetAsync(id);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        public Task<PagedResultDto<PurchaseReturnHeaderDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return _purchaseReturnHeaderAppService.GetListAsync(input);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public Task<PurchaseReturnHeaderDto> UpdateAsync(Guid id, CreateUpdatePurchaseReturnHeaderDto input)
        {
            return _purchaseReturnHeaderAppService.UpdateAsync(id, input);
        }


        
    }
}

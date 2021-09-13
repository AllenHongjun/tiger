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

namespace Tiger.Controllers.Admin.PurchaseHeaders
{
    /// <summary>
    /// 出库单
    /// </summary>

    [RemoteService(Name = "PurchaseHeader")]
    [Area("Purchase")]
    [ControllerName("PurchaseHeader")]
    [Route("api/purchase/purchaseHeader")]
    [ApiExplorerSettings(GroupName = "admin-erp")]
    public class PurchaseHeaderController : TigerController
    {
        protected readonly IPurchaseHeaderAppService _purchaseHeaderAppService;

        public PurchaseHeaderController(IPurchaseHeaderAppService purchaseHeaderAppService)
        {
            _purchaseHeaderAppService = purchaseHeaderAppService;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<PurchaseHeaderDto> CreateAsync(CreateUpdatePurchaseHeaderDto input)
        {
            return _purchaseHeaderAppService.CreateAsync(input);
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
            return _purchaseHeaderAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 获取单条
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public Task<PurchaseHeaderDto> GetAsync(Guid id)
        {
            return _purchaseHeaderAppService.GetAsync(id);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        public Task<PagedResultDto<PurchaseHeaderDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return _purchaseHeaderAppService.GetListAsync(input);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public Task<PurchaseHeaderDto> UpdateAsync(Guid id, CreateUpdatePurchaseHeaderDto input)
        {
            return _purchaseHeaderAppService.UpdateAsync(id, input);
        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("audit/{id}")]
        public Task AuditAsync(Guid id)
        {
            return Task.CompletedTask;
        }


        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("batchDelete")]
        public override Task BatchDeleteAsync(Guid[] ids)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// 批量审核
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("batchAudit")]
        public override Task BatchAuditAsync(Guid[] ids)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// 批量关闭
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("batchClose")]
        public override Task BatchCloseAsync(Guid[] ids)
        {
            return Task.CompletedTask;
        }



    }
}

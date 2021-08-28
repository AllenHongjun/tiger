using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Tiger.Business.Members;
using Tiger.Business.Members.Dtos;
using Tiger.Business.Stocks;
using Tiger.Business.Stocks.Dtos;
using Tiger.Stock;
using Tiger.Stock.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Tiger.Controllers.Admin.ReceiptHeaders
{
    /// <summary>
    /// 出库单
    /// </summary>

    [RemoteService(Name = "ReceiptHeader")]
    [Area("Basics")]
    [ControllerName("ReceiptHeader")]
    [Route("api/basic/receiptHeader")]
    [ApiExplorerSettings(GroupName = "admin-erp")]
    public class ReceiptHeaderController : TigerController, IReceiptHeaderAppService
    {
        protected readonly IReceiptHeaderAppService _receiptHeaderAppService;

        public ReceiptHeaderController(IReceiptHeaderAppService receiptHeaderAppService)
        {
            _receiptHeaderAppService = receiptHeaderAppService;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<ReceiptHeaderDto> CreateAsync(CreateUpdateReceiptHeaderDto input)
        {
            return _receiptHeaderAppService.CreateAsync(input);
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
            return _receiptHeaderAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 获取单条
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public Task<ReceiptHeaderDto> GetAsync(Guid id)
        {
            return _receiptHeaderAppService.GetAsync(id);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        public Task<PagedResultDto<ReceiptHeaderDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return _receiptHeaderAppService.GetListAsync(input);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public Task<ReceiptHeaderDto> UpdateAsync(Guid id, CreateUpdateReceiptHeaderDto input)
        {
            return _receiptHeaderAppService.UpdateAsync(id, input);
        }


        
    }
}

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

namespace Tiger.Controllers.Admin.TransferHeaders
{
    /// <summary>
    /// 出库单
    /// </summary>

    [RemoteService(Name = "TransferHeader")]
    [Area("Stocks")]
    [ControllerName("TransferHeader")]
    [Route("api/stock/transferHeader")]
    [ApiExplorerSettings(GroupName = "admin-erp")]
    public class TransferHeaderController : TigerController, ITransferHeaderAppService
    {
        protected readonly ITransferHeaderAppService _transferHeaderAppService;

        public TransferHeaderController(ITransferHeaderAppService transferHeaderAppService)
        {
            _transferHeaderAppService = transferHeaderAppService;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<TransferHeaderDto> CreateAsync(CreateUpdateTransferHeaderDto input)
        {
            return _transferHeaderAppService.CreateAsync(input);
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
            return _transferHeaderAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 获取单条
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public Task<TransferHeaderDto> GetAsync(Guid id)
        {
            return _transferHeaderAppService.GetAsync(id);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        public Task<PagedResultDto<TransferHeaderDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return _transferHeaderAppService.GetListAsync(input);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public Task<TransferHeaderDto> UpdateAsync(Guid id, CreateUpdateTransferHeaderDto input)
        {
            return _transferHeaderAppService.UpdateAsync(id, input);
        }


        
    }
}

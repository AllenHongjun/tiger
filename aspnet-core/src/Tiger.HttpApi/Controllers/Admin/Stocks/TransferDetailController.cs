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

namespace Tiger.Controllers.Admin.TransferDetails
{
    /// <summary>
    /// 出库单
    /// </summary>

    [RemoteService(Name = "TransferDetail")]
    [Area("Basics")]
    [ControllerName("TransferDetail")]
    [Route("api/basic/transferDetail")]
    [ApiExplorerSettings(GroupName = "admin-erp")]
    public class TransferDetailController : TigerController, ITransferDetailAppService
    {
        protected readonly ITransferDetailAppService _transferDetailAppService;

        public TransferDetailController(ITransferDetailAppService transferDetailAppService)
        {
            _transferDetailAppService = transferDetailAppService;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<TransferDetailDto> CreateAsync(CreateUpdateTransferDetailDto input)
        {
            return _transferDetailAppService.CreateAsync(input);
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
            return _transferDetailAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 获取单条
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public Task<TransferDetailDto> GetAsync(Guid id)
        {
            return _transferDetailAppService.GetAsync(id);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        public Task<PagedResultDto<TransferDetailDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return _transferDetailAppService.GetListAsync(input);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public Task<TransferDetailDto> UpdateAsync(Guid id, CreateUpdateTransferDetailDto input)
        {
            return _transferDetailAppService.UpdateAsync(id, input);
        }


        
    }
}

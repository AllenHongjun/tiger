using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Tiger.Stock;
using Tiger.Stock.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Tiger.Controllers.Admin.CheckHeaders
{
    /// <summary>
    /// 实时库存
    /// </summary>

    [RemoteService(Name = "CheckHeader")]
    [Area("Basics")]
    [ControllerName("CheckHeader")]
    [Route("api/basic/checkHeader")]
    [ApiExplorerSettings(GroupName = "admin-erp")]
    public class CheckHeaderController : TigerController, ICheckHeaderAppService
    {
        protected readonly ICheckHeaderAppService _checkHeaderAppService;

        public CheckHeaderController(ICheckHeaderAppService checkHeaderAppService)
        {
            _checkHeaderAppService = checkHeaderAppService;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<CheckHeaderDto> CreateAsync(CreateUpdateCheckHeaderDto input)
        {
            return _checkHeaderAppService.CreateAsync(input);
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
            return _checkHeaderAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 获取单条
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public Task<CheckHeaderDto> GetAsync(Guid id)
        {
            return _checkHeaderAppService.GetAsync(id);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        public Task<PagedResultDto<CheckHeaderDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return _checkHeaderAppService.GetListAsync(input);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public Task<CheckHeaderDto> UpdateAsync(Guid id, CreateUpdateCheckHeaderDto input)
        {
            return _checkHeaderAppService.UpdateAsync(id, input);
        }


        
    }
}

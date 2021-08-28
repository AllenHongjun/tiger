using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Tiger.Stock;
using Tiger.Stock.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Tiger.Controllers.Admin.CheckDetails
{
    /// <summary>
    /// 实时库存
    /// </summary>

    [RemoteService(Name = "CheckDetail")]
    [Area("Basics")]
    [ControllerName("CheckDetail")]
    [Route("api/basic/checkDetail")]
    [ApiExplorerSettings(GroupName = "admin-erp")]
    public class CheckDetailController : TigerController, ICheckDetailAppService
    {
        protected readonly ICheckDetailAppService _checkDetailAppService;

        public CheckDetailController(ICheckDetailAppService checkDetailAppService)
        {
            _checkDetailAppService = checkDetailAppService;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<CheckDetailDto> CreateAsync(CreateUpdateCheckDetailDto input)
        {
            return _checkDetailAppService.CreateAsync(input);
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
            return _checkDetailAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 获取单条
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public Task<CheckDetailDto> GetAsync(Guid id)
        {
            return _checkDetailAppService.GetAsync(id);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        public Task<PagedResultDto<CheckDetailDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return _checkDetailAppService.GetListAsync(input);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public Task<CheckDetailDto> UpdateAsync(Guid id, CreateUpdateCheckDetailDto input)
        {
            return _checkDetailAppService.UpdateAsync(id, input);
        }


        
    }
}

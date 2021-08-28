using Microsoft.AspNetCore.Mvc;
using System;
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

    [RemoteService(Name = "Supply")]
    [Area("Basics")]
    [ControllerName("Supply")]
    [Route("api/basic/supply")]
    [ApiExplorerSettings(GroupName = "admin-basic")]
    public class SupplyController : TigerController, ISupplyAppService
    {
        protected readonly ISupplyAppService _supplyAppService;

        public SupplyController(ISupplyAppService supplyAppService)
        {
            _supplyAppService = supplyAppService;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<SupplyDto> CreateAsync(CreateUpdateSupplyDto input)
        {
            return _supplyAppService.CreateAsync(input);
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
            return _supplyAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 获取单条
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public Task<SupplyDto> GetAsync(Guid id)
        {
            return _supplyAppService.GetAsync(id);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        //[Route("all")]
        public Task<PagedResultDto<SupplyDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return _supplyAppService.GetListAsync(input);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public Task<SupplyDto> UpdateAsync(Guid id, CreateUpdateSupplyDto input)
        {
            return _supplyAppService.UpdateAsync(id, input);
        }


        

        
    }
}

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

    [RemoteService(Name = "Sku")]
    [Area("Basics")]
    [ControllerName("Sku")]
    [Route("api/basic/sku")]
    [ApiExplorerSettings(GroupName = "admin-basic")]
    public class SkuController : TigerController, ISkuAppService
    {
        protected readonly ISkuAppService _skuAppService;

        public SkuController(ISkuAppService skuAppService)
        {
            _skuAppService = skuAppService;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<SkuDto> CreateAsync(CreateUpdateSkuDto input)
        {
            return _skuAppService.CreateAsync(input);
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
            return _skuAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 获取单条
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public Task<SkuDto> GetAsync(Guid id)
        {
            return _skuAppService.GetAsync(id);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        //[Route("all")]
        public Task<PagedResultDto<SkuDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return _skuAppService.GetListAsync(input);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public Task<SkuDto> UpdateAsync(Guid id, CreateUpdateSkuDto input)
        {
            return _skuAppService.UpdateAsync(id, input);
        }


        

        
    }
}

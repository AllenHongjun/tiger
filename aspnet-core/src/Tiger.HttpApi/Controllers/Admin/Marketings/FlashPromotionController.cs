using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Marketing;
using Tiger.Marketing.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Identity;

namespace Tiger.Controllers.Admin.Basics
{
    /// <summary>
    /// 优惠券
    /// </summary>

    [RemoteService(Name = "admin-basic")]
    [Area("Marketings")]
    [ControllerName("FlashPromotion")]
    [Route("api/basic/flashPromotion")]
    [ApiExplorerSettings(GroupName = "admin-basic")]
    public class FlashPromotionController : TigerController, IFlashPromotionAppService
    {
        protected readonly IFlashPromotionAppService _flashPromotionAppService;

        public FlashPromotionController(IFlashPromotionAppService flashPromotionAppService)
        {
            _flashPromotionAppService = flashPromotionAppService;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<FlashPromotionDto> CreateAsync(CreateUpdateFlashPromotionDto input)
        {
            return _flashPromotionAppService.CreateAsync(input);
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
            return _flashPromotionAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 获取单条
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public Task<FlashPromotionDto> GetAsync(Guid id)
        {
            return _flashPromotionAppService.GetAsync(id);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        //[Route("all")]
        public Task<PagedResultDto<FlashPromotionDto>> GetListAsync(PagedAndSortedResultRequestDto  input)
        {
            return _flashPromotionAppService.GetListAsync(input);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public Task<FlashPromotionDto> UpdateAsync(Guid id, CreateUpdateFlashPromotionDto input)
        {
            return _flashPromotionAppService.UpdateAsync(id, input);
        }


        
    }
}

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
    [ControllerName("FlashPromotionSession")]
    [Route("api/basic/flashPromotionSession")]
    [ApiExplorerSettings(GroupName = "admin-basic")]
    public class FlashPromotionSessionController : TigerController, IFlashPromotionSessionAppService
    {
        protected readonly IFlashPromotionSessionAppService _flashPromotionSessionAppService;

        public FlashPromotionSessionController(IFlashPromotionSessionAppService flashPromotionSessionAppService)
        {
            _flashPromotionSessionAppService = flashPromotionSessionAppService;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<FlashPromotionSessionDto> CreateAsync(CreateUpdateFlashPromotionSessionDto input)
        {
            return _flashPromotionSessionAppService.CreateAsync(input);
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
            return _flashPromotionSessionAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 获取单条
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public Task<FlashPromotionSessionDto> GetAsync(Guid id)
        {
            return _flashPromotionSessionAppService.GetAsync(id);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        //[Route("all")]
        public Task<PagedResultDto<FlashPromotionSessionDto>> GetListAsync(PagedAndSortedResultRequestDto  input)
        {
            return _flashPromotionSessionAppService.GetListAsync(input);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public Task<FlashPromotionSessionDto> UpdateAsync(Guid id, CreateUpdateFlashPromotionSessionDto input)
        {
            return _flashPromotionSessionAppService.UpdateAsync(id, input);
        }


        
    }
}

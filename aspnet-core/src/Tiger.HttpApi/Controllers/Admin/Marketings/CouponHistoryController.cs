using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Marketings.CouponHistorys;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Identity;

namespace Tiger.Controllers.Admin.Basics
{
    /// <summary>
    /// 优惠券使用记录
    /// </summary>

    [RemoteService(Name = "admin-basic")]
    [Area("Marketings")]
    [ControllerName("CouponHistory")]
    [Route("api/marketing/couponHistory")]
    [ApiExplorerSettings(GroupName = "admin-basic")]
    public class CouponHistoryController : TigerController, ICouponHistoryAppService
    {
        protected readonly ICouponHistoryAppService _couponHistoryAppService;

        public CouponHistoryController(ICouponHistoryAppService couponHistoryAppService)
        {
            _couponHistoryAppService = couponHistoryAppService;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<CouponHistoryDto> CreateAsync(CreateUpdateCouponHistoryDto input)
        {
            return _couponHistoryAppService.CreateAsync(input);
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
            return _couponHistoryAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 获取单条
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public Task<CouponHistoryDto> GetAsync(Guid id)
        {
            return _couponHistoryAppService.GetAsync(id);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        //[Route("all")]
        public Task<PagedResultDto<CouponHistoryDto>> GetListAsync(GetCouponHistoryListDto input)
        {
            return _couponHistoryAppService.GetListAsync(input);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        
        public Task<CouponHistoryDto> UpdateAsync(Guid id, CreateUpdateCouponHistoryDto input)
        {
            return _couponHistoryAppService.UpdateAsync(id, input);
        }


        
    }
}

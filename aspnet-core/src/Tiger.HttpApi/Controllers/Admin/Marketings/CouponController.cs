using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Marketings.Coupons;
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
    [ControllerName("Coupon")]
    [Route("api/marketing/coupon")]
    [ApiExplorerSettings(GroupName = "admin-basic")]
    public class CouponController : TigerController, ICouponAppService
    {
        protected readonly ICouponAppService _couponAppService;

        public CouponController(ICouponAppService couponAppService)
        {
            _couponAppService = couponAppService;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<CouponDto> CreateAsync(CreateUpdateCouponDto input)
        {
            return _couponAppService.CreateAsync(input);
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
            return _couponAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 获取单条
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public Task<CouponDto> GetAsync(Guid id)
        {
            return _couponAppService.GetAsync(id);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        //[Route("all")]
        public Task<PagedResultDto<CouponDto>> GetListAsync(GetCouponListDto input)
        {
            return _couponAppService.GetListAsync(input);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public Task<CouponDto> UpdateAsync(Guid id, CreateUpdateCouponDto input)
        {
            return _couponAppService.UpdateAsync(id, input);
        }


        
    }
}

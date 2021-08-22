﻿/**
 * 类    名：Class1   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/11 8:04:39       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Business.Marketings;
using Tiger.Marketing;
using Tiger.Marketings.Coupons;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Module.Marketings
{
    public class CouponAppService
        : CrudAppService<
            Coupon, //The  entity
            CouponDto,
            Guid, //Primary key 
            GetCouponListDto,
            CreateUpdateCouponDto, //Used for paging/sorting
            CreateUpdateCouponDto>, //Used to create/update
        ICouponAppService
    {
        private readonly ICounponRepository _counponRepository;
        public CouponAppService(ICounponRepository counponRepository) : base(counponRepository)
        {
            _counponRepository = counponRepository;
        }
    }
}

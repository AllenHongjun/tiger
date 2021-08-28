using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Services;

namespace Tiger.Marketings.Coupons
{
    public interface ICouponAppService
        : ICrudAppService<
            CouponDto, // Used to show category
            Guid,
            GetCouponListDto,
            CreateUpdateCouponDto,
            CreateUpdateCouponDto>
    {
    }
}

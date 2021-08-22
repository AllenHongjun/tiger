using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Tiger.Marketings.Coupons
{
    public class GetCouponListDto : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 查询关键字
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// 优惠券类型 0 全场赠券； 1 会员赠券； 2 购物赠券； 3 注册赠券；
        /// </summary>
        public int Type { get; set; }
    }
}

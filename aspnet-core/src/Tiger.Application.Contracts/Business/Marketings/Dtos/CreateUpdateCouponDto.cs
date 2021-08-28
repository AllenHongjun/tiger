using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Marketings.Coupons
{
    public class CreateUpdateCouponDto
    {
        /// <summary>
        /// 优惠券类型 0 全场赠券； 1 会员赠券； 2 购物赠券； 3 注册赠券；
        /// </summary>
        public int Type { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// 使用平台 0 全部 1 移动 2 PC
        /// </summary>
        public int Platform { get; set; }


        public int Count { get; set; }

        public decimal Amount { get; set; }

        public int PerLimit { get; set; }

        /// <summary>
        /// 使用门槛 0 表示无门槛
        /// </summary>
        public decimal MinPoint { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        /// <summary>
        /// 使用类型 0 全场通用 ； 1 指定分类  2 指定商品
        /// </summary>
        public int UseType { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; set; }



        public int ReceiveCount { get; set; }

        public DateTime EnableTime { get; set; }

        public string Code { get; set; }

        /// <summary>
        /// 可以领取的会员类型 0 无限制
        /// </summary>
        public int MemberLevel { get; set; }
    }
}

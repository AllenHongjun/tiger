using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Marketings.CouponHistorys
{
    public class CreateUpdateCouponHistoryDto
    {
        public Guid CouponId { get; set; }

        public Guid MemberId { get; set; }

        /// <summary>
        /// 优惠券编码
        /// </summary>
        public string CouponCode { get; set; }

        public string MemberNickName { get; set; }

        /// <summary>
        /// 获取类型：0->后台赠送；1->主动获取
        /// </summary>
        public new int GetType { get; set; }

        //public DateTime CreateTime { get; set; }

        public int UseStatus { get; set; }

        public DateTime UseTime { get; set; }

        public Guid OrderId { get; set; }

        /// <summary>
        /// 订单编码
        /// </summary>
        public string OrderSn { get; set; }
    }
}

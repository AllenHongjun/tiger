using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Tiger.Marketings.CouponHistorys
{
    public class CouponHistoryDto:EntityDto<Guid>
    {
        public Guid CouponId { get; set; }

        public Guid MemberId { get; set; }

        /// <summary>
        /// 优惠券编码
        /// </summary>
        public string CouponCode { get; set; }

        /// <summary>
        /// 领券用户昵称
        /// </summary>
        public string MemberNickName { get; set; }

        /// <summary>
        /// 获取类型：0->后台赠送；1->主动获取
        /// </summary>
        public int GetType { get; set; }

        //public DateTime CreateTime { get; set; }

        /// <summary>
        /// 使用状态
        /// </summary>
        public int UseStatus { get; set; }

        public DateTime UseTime { get; set; }

        public Guid OrderId { get; set; }

        /// <summary>
        /// 订单编码
        /// </summary>
        public string OrderSn { get; set; }
    }
}

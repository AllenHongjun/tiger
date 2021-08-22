/**
 * 类    名：CouponHistory   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/11 13:46:50       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Business.Users;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tiger.Marketing
{   
    /// <summary>
    /// 优惠券使用记录表 
    /// 
    /// 用户和优惠券关系表
    /// </summary>
    public class CouponHistory : AuditedAggregateRoot<Guid>
    {
        public Guid CouponId { get; set; }

        public virtual Coupon Coupon { get; set; }

        public Guid MemberId { get; set; }

        public virtual Member Member { get; set; }
        
        /// <summary>
        /// 优惠券编码
        /// </summary>
        public string CouponCode { get; set; }

        /// <summary>
        /// 领取人昵称
        /// </summary>
        public string MemberNickName { get; set; }

        /// <summary>
        /// 获取类型：0->后台赠送；1->主动获取
        /// </summary>
        public int GetType { get; set; }

        //public DateTime CreateTime { get; set; }

        /// <summary>
        /// 使用状态：0->未使用；1->已使用；2->已过期
        /// </summary>
        public int UseStatus { get; set; }

        /// <summary>
        /// 使用时间
        /// </summary>
        public DateTime UseTime { get; set; }

        public Guid OrderId { get; set; }

        /// <summary>
        /// 订单编码
        /// </summary>
        public string OrderSn { get; set; }
    }
}

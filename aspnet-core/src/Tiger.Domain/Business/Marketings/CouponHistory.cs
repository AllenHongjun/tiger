/**
 * 类    名：CouponHistory   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/11 13:46:50       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tiger.Business.Members;
using Tiger.Business.Orders;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Tiger.Marketing
{   
    /// <summary>
    /// 优惠券使用记录表 
    /// 
    /// 用户和优惠券关系表
    /// </summary>
    public class CouponHistory : AuditedAggregateRoot<Guid>,IMultiTenant
    {
        
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
        public int Type { get; set; }

        /// <summary>
        /// 使用状态：0->未使用；1->已使用；2->已过期
        /// </summary>
        public int UseStatus { get; set; }

        /// <summary>
        /// 使用时间
        /// </summary>
        public DateTime UseTime { get; set; }

        /// <summary>
        /// 如果有合并支付就管理 主单id
        /// </summary>
        //public Guid MasterOrderId { get; set; }

        /// <summary>
        /// 订单编码
        /// </summary>
        public string OrderSn { get; set; }

        public Guid CouponId { get; set; }

        [ForeignKey("CouponId")]
        public virtual Coupon Coupon { get; set; }

        public Guid MemberId { get; set; }
        [ForeignKey("MemberId")]
        public virtual Member Member { get; set; }

        public Guid OrderId { get; set; }

        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }

        public Guid? TenantId { get; set; }
    }
}

/**
 * 类    名：CouponProduct   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/11 13:47:22       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tiger.Basic;
using Volo.Abp.Domain.Entities;

namespace Tiger.Marketing
{
    /// <summary>
    /// 优惠券商品关系表
    /// </summary>
    public class CouponProductRelation:Entity<Guid>
    {
        public Guid CouponId { get; set; }

        [ForeignKey("CouponId")]
        public virtual Coupon Coupon { get; set; }

        public Guid ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        public string ProductName { get; set; }

        public string ProductSn { get; set; }
    }
}

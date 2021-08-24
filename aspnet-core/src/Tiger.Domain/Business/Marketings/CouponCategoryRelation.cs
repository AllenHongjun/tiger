/**
 * 类    名：CouponCategory   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/11 13:48:14       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Basic;
using Volo.Abp.Domain.Entities;

namespace Tiger.Marketing
{   
    /// <summary>
    /// 优惠券分类关系表
    /// </summary>
    public class CouponCategoryRelation:Entity<Guid>
    {
        public Guid CouponId { get; set; }

        public virtual Coupon Coupon { get; set; }

        public Guid CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public string CategoryName { get; set; }

        public string ParentCategoryName { get; set; }
    }
}

/**
 * 类    名：OrderItem   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/11 13:28:55       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Basic;
using Tiger.Business;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Tiger.Orders
{
    /// <summary>
    /// 订单明细
    /// </summary>
    public class OrderItem : FullAuditedAggregateRoot<Guid>,ISoftDelete,IMultiTenant
    {
        public Guid? TenantId { get; set; }

        public int OrderId { get; set; }

        public virtual Order Order { get; set; }

        /// <summary>
        /// 订单编码
        /// </summary>
        public string OrderSn { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }

        public string ProductPic { get; set; }

        public string ProductName { get; set; }

        public String ProductBrand { get; set; }

        public string ProductSn { get; set; }

        /// <summary>
        /// 商品销售价格
        /// </summary>
        public decimal ProductPrice { get; set; }

        public int ProductQuantity { get; set; }

        public int ProductSkuId { get; set; }

        public string ProductSkuCode { get; set; }

        public int ProductCategoryId { get; set; }

        public string PromotionName { get; set; }

        /// <summary>
        /// 促销金额
        /// </summary>
        public Decimal PromotionAmount { get; set; }

        /// <summary>
        /// 优惠券金额
        /// </summary>
        public decimal CouponAmount { get; set; }

        /// <summary>
        /// 积分金额
        /// </summary>
        public decimal IntegrationAmount { get; set; }

        /// <summary>
        /// 实付金额
        /// </summary>
        public decimal RealAmount { get; set; }

        /// <summary>
        /// 商品赠送积分
        /// </summary>
        public decimal GiftIntegration { get; set; }

        /// <summary>
        /// 商品赠送成长值
        /// </summary>
        public decimal GiftGrowth { get; set; }

        /// <summary>
        /// 商品销售属性
        /// </summary>
        /// <remarks>
        /// '商品销售属性:[{"key":"颜色","value":"颜色"},{"key":"容量","value":"4G"}]',
        /// 
        /// </remarks>
        public string ProductAttr { get; set; }

        
    }
}

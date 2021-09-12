using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Basic.Products;
using Volo.Abp.Application.Dtos;

namespace Tiger.Orders.OrderItems
{
    public class OrderItemDto:AuditedEntityDto<Guid>
    {
        /// <summary>
        /// 订单编码
        /// </summary>
        public string OrderSn { get; set; }

        public string ProductPic { get; set; }

        public string ProductName { get; set; }

        public String ProductBrand { get; set; }

        public string ProductSn { get; set; }

        /// <summary>
        /// 商品销售价格
        /// </summary>
        public decimal ProductPrice { get; set; }

        /// <summary>
        /// 购买数量
        /// </summary>
        public int ProductQuantity { get; set; }

        /// <summary>
        /// 商品sku条码
        /// </summary>
        public string ProductSkuCode { get; set; }

        public Guid ProductCategoryId { get; set; }

        /// <summary>
        /// 商品促销名称
        /// </summary>
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


        /// <summary>
        /// 商品sku编号
        /// </summary>
        public Guid SkuId { get; set; }



        public Guid OrderId { get; set; }



        public Guid ProductId { get; set; }

        public ProductDto product { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Tiger.Orders.CartItems
{
    public class CartItemDto:AuditedEntityDto<Guid>
    {
        public Guid ProductId { get; set; }


        public Guid SkuId { get; set; }

        public Guid MemberId { get; set; }

        public Guid CategoryId { get; set; }

        /// <summary>
        /// 购买数量
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// 添加到购物车的价格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 商品主图
        /// </summary>
        public string ProductPic { get; set; }

        public string ProductName { get; set; }

        public string ProductSubTitle { get; set; }

        /// <summary>
        /// 商品sku条码
        /// </summary>
        public string SkuCode { get; set; }

        public string MemberNickName { get; set; }


        /// <summary>
        /// 品牌名称
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        /// 货号
        /// </summary>
        public string ProductSn { get; set; }

        /// <summary>
        /// 商品销售属性:[{"key":"颜色","value":"颜色"},{"key":"容量","value":"4G"}]
        /// </summary>
        public string ProductAttr { get; set; }
    }
}

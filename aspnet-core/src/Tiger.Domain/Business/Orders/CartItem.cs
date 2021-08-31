/**
 * 类    名：CartItem   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/11 13:30:23       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tiger.Basic;
using Tiger.Business.Members;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tiger.Orders
{   
    /// <summary>
    /// 购物车明细表(用户和商品关系表)
    /// </summary>
    public class CartItem:FullAuditedAggregateRoot<Guid>
    {
        

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

        

        public string Brand { get; set; }

        /// <summary>
        /// 货号
        /// </summary>
        public string ProductSn { get; set; }

        /// <summary>
        /// 商品销售属性:[{"key":"颜色","value":"颜色"},{"key":"容量","value":"4G"}]
        /// </summary>
        public string ProductAttr { get; set; }

        

        public Guid? SkuId { get; set; }

        public virtual Sku Sku { get; set; }
        

        public Guid ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        public Guid MemberId { get; set; }

        [ForeignKey("MemberId")]
        public virtual Member Member { get; set; }

        public Guid CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        
    }
}

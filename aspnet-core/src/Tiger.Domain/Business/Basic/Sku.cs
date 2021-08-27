/**
 * 类    名：Sku   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/11 13:57:15       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tiger.Basic
{   
    /// <summary>
    /// 库存量单位(pms_sku_stock)
    /// </summary>
    public class Sku: AuditedAggregateRoot<Guid>
    {

        /// <summary>
        /// SKU编码
        /// </summary>
        public string SkuCode { get; set; }

        public decimal Price { get; set; }

        /// <summary>
        /// 库存量
        /// </summary>
        public decimal Stock { get; set; }

        /// <summary>
        /// 预警库存
        /// </summary>
        public decimal LowStock { get; set; }

        /// <summary>
        /// 商品属性数据
        /// 
        /// [{"key":"颜色","value":"红色"},{"key":"尺寸","value":"38"},{"key":"风格","value":"夏季"}]
        /// [{"key":"颜色","value":"银色"},{"key":"容量","value":"128G"}]
        /// </summary>
        public string SPData { get; set; }

        /// <summary>
        /// 销量
        /// </summary>
        public int Sale { get; set; }

        /// <summary>
        /// 展示图片
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// 单品促销价格
        /// </summary>
        public decimal PromotionPrice { get; set; }

        /// <summary>
        /// 锁定库存
        /// </summary>
        public int LockStock { get; set; } = 0;

        public Guid ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}

/**
 * 类    名：Sku   
 * 作    者：hongjy       
 * 创建时间：2021/8/11 13:57:15       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace TigerAdmin.Basic
{   
    /// <summary>
    /// 库存量单位(pms_sku_stock)
    /// </summary>
    public class Sku
    {
        public int ProductId { get; set; }

        public string SkuCode { get; set; }

        public decimal Price { get; set; }

        /// <summary>
        /// 库存量
        /// </summary>
        public decimal Stock { get; set; }

        public string SP1 { get; set; }

        public string SP2 { get; set; }

        public string SP3 { get; set; }

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
    }
}

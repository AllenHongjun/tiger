/**
 * 类    名：LocationInventoryHis   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/11 15:05:21       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tiger.Basic;

namespace Tiger.Stock
{   
    /// <summary>
    /// 库存变动历史记录
    /// </summary>
    public class InventoryHistory
    {
        public string ProductName { get; set; }

        /// <summary>
        /// 在库数量
        /// 
        /// 重量精确到克， 金额精确到分 精确到最小的那个单位来计算
        /// </summary>
        public int OnHandQty { get; set; }

        /// <summary>
        /// 变动数量
        /// </summary>
        public int TransitQty { get; set; }

        /// <summary>
        /// 分配数量
        /// </summary>
        public int AllocateQty { get; set; }

        public int LockedQty { get; set; }

        /// <summary>
        /// 冻结数量
        /// </summary>
        public int FrozenQty { get; set; }

        /// <summary>
        /// 库存状态
        /// </summary>
        public int InventoryStatus { get; set; }

        public string AttributeData { get; set; }

        public string Batch { get; set; }

        /// <summary>
        /// 生产日期
        /// </summary>
        public DateTime ManufactureDate { get; set; }

        /// <summary>
        /// 失效日期
        /// </summary>
        public DateTime ExpirationDate { get; set; }

        /// <summary>
        /// 变动日期
        /// </summary>
        public DateTime AgingDate { get; set; }

        /// <summary>
        /// 箱换算数量
        /// </summary>
        public int CsQty { get; set; }

        public Guid WarehouseId { get; set; }

        [ForeignKey("WarehouseId")]
        public virtual Warehouse Warehouse { get; set; }

        public Guid ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }


        
    }
}

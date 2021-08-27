/**
 * 类    名：LocaationInventory   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/11 15:04:52       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tiger.Basic;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tiger.Stock
{
    /// <summary>
    /// 库存表
    /// </summary>
    public class Inventory: FullAuditedAggregateRoot<Guid>, ISoftDelete
    {
        

        public string ProductName { get; set; }

        /// <summary>
        /// 在库数量
        /// 
        /// 重量精确到克， 金额精确到分 精确到最小的那个单位来计算
        /// </summary>
        public int OnHandQty { get; set; }

        ///// <summary>
        ///// 移入数量
        ///// </summary>
        //public int TransitQty { get; set; }

        ///// <summary>
        ///// 分配数量
        ///// </summary>
        //public int AllocateQty { get; set; }

        public int LockedQty { get; set; }

        /// <summary>
        /// 冻结数量
        /// </summary>
        public int FrozenQty { get; set; }

        /// <summary>
        /// 库存状态
        /// </summary>
        public int InventoryStatus { get; set; }

        /// <summary>
        /// 商品属性
        /// </summary>
        public string AttributeData { get; set; }
       

        /// <summary>
        /// 箱换算数量
        /// </summary>
        public int CsQty { get; set; }

        public Guid WarehouseId { get; set; }

        [ForeignKey("WarehouseId")]
        public virtual Warehouse Warehouse { get; set; }

        [ForeignKey("ProductId")]
        public Guid ProductId { get; set; }

        public virtual Product Product { get; set; }

        //public Guid SkuId { get; set; }


    }
}

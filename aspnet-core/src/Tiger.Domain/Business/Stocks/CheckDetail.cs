/**
 * 类    名：CheckDetail   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/11 15:23:46       
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
using Volo.Abp.MultiTenancy;

namespace Tiger.Stock
{
    /// <summary>
    /// 盘点单明细
    /// </summary>
    [Table("StockCheckDetail")]
    public class CheckDetail : FullAuditedAggregateRoot<Guid>, ISoftDelete, IMultiTenant
    {



        public string ProductSn { get; set; }

        public string ProductName { get; set; }

        /// <summary>
        /// 库存状态
        /// </summary>
        public int InventorySts { get; set; }

        /// <summary>
        /// 系统数量
        /// </summary>
        public int SystemQty { get; set; }

        /// <summary>
        /// 上次实盘数量
        /// </summary>
        public int LastCheckQty { get; set; }

        /// <summary>
        /// 实盘数量
        /// </summary>
        public int CheckedQty { get; set; }

        /// <summary>
        /// 盘点创建人
        /// </summary>
        public DateTime CheckedBy { get; set; }

        /// <summary>
        /// 盘点日期
        /// </summary>
        public DateTime CheckedAt { get; set; }

        /// <summary>
        /// 处理标记
        /// </summary>
        public string ProcessStamp { get; set; }

        /// <summary>
        /// 调整数量
        /// </summary>
        public int AdjustQty { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        public string Batch { get; set; }


        public Guid WarehouseId { get; set; }

        [ForeignKey("WarehouseId")]
        public virtual Warehouse Warehouse { get; set; }

        
        public Guid ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

        public Guid CheckHeaderId { get; set; }

        [ForeignKey("CheckHeaderId")]
        public virtual CheckHeader CheckHeader { get; set; }

        public Guid? TenantId { get; set; }
    }
}

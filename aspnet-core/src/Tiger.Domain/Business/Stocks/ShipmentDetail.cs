using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tiger.Basic;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Tiger.Business.Stocks
{
    /// <summary>
    /// 出库单明细
    /// </summary>
    [Table("StockShipmentDetail")]
    public class ShipmentDetail : FullAuditedAggregateRoot<Guid>, ISoftDelete, IMultiTenant
    {

        public string ProductName { get; set; }

        public string ProductSn { get; set; }

        /// <summary>
        /// 发货数量
        /// </summary>
        public int ShipQty { get; set; }

        /// <summary>
        /// 请求数量
        /// </summary>
        public int RequestQty { get; set; }

        /// <summary>
        /// 以取消数量
        /// </summary>
        public int CanceledQty { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        public string Batch { get; set; }

        /// <summary>
        /// 出库日期
        /// </summary>
        public DateTime AgingDate { get; set; }

        /// <summary>
        /// 库存状态
        /// </summary>
        public int InventorySts { get; set; }

        /// <summary>
        /// 总重量
        /// </summary>
        public decimal TotalWeight { get; set; }

        /// <summary>
        /// 总体积
        /// </summary>
        public decimal TotalVolume { get; set; }

        /// <summary>
        /// 处理标记
        /// </summary>
        public string ProcessStamp { get; set; }

        /// <summary>
        /// 数量单位
        /// </summary>
        public string Unit { get; set; }

        


        [ForeignKey("WarehouseId")]
        public virtual Warehouse Warehouse { get; set; }
        
        public Guid ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        
        public Guid ShipmentId { get; set; }

        [ForeignKey("ShipmentId")]
        public virtual ShipmentHeader ShipmentHeader { get; set; }


        public Guid? TenantId { get; set; }
    }
}

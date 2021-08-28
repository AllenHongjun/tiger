/**
 * 类    名：ReceiptDetail   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/11 15:26:29       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tiger.Basic;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Tiger.Stock
{
    /// <summary>
    /// 入库单明细
    /// </summary>
    [Table("StockReceiptDetail")]
    public class ReceiptDetail : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {   
        

        /// <summary>
        /// 入库单号
        /// </summary>
        public string ReceiptCode { get; set; }

        /// <summary>
        /// 仓库编码
        /// </summary>
        public string WarehouseCode { get; set; }

        /// <summary>
        /// 商品货号
        /// </summary>
        public string ProductSn { get; set; }

        public string ProductName { get; set; }

        /// <summary>
        /// 批次
        /// </summary>
        public string Batch { get; set; }

        /// <summary>
        /// 生产日期
        /// </summary>
        public DateTime ManufactureDate { get; set; }

        /// <summary>
        /// 入库日期
        /// </summary>
        public DateTime AgingDate { get; set; }

        /// <summary>
        /// 总数量
        /// </summary>
        public int TotalQty { get; set; }

        /// <summary>
        /// 未收数量
        /// </summary>
        public int OpenQty { get; set; }

        /// <summary>
        /// 处理标记
        /// </summary>
        public string ProcessStamp { get; set; }

        /// <summary>
        /// 数量单位
        /// </summary>
        public string QuantityUm { get; set; }

        [ForeignKey("WarehouseId")]
        public virtual Warehouse Warehouse { get; set; }

        [ForeignKey("ProductId")]
        public Guid ProductId { get; set; }

        public virtual Product Product { get; set; }

        /// <summary>
        /// 入库单Id
        /// </summary>
        public Guid ReceiptHeaderId { get; set; }

        [ForeignKey("ReceiptHeaderId")]
        public virtual ReceiptHeader ReceiptHeader { get; set; }



        public Guid? TenantId { get; set; }
    }
}

/**
 * 类    名：ReceiptHeader   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/11 15:25:59       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tiger.Business.Stocks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Tiger.Stock
{
    /// <summary>
    /// 入库单
    /// </summary>
    [Table("StockReceiptHeader")]
    public class ReceiptHeader : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {   

        /// <summary>
        /// 入库单号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 入库单类型 0->其他入库 1->采购入库 2->退货入库 3-> 盘盈入库 
        /// </summary>
        public int ReceiptType { get; set; }


        /// <summary>
        /// 采购单号
        /// </summary>
        public Guid? PurchaseOrderId { get; set; }

        /// <summary>
        /// 实际到达时间
        /// </summary>
        public DateTime ArriveDatetime { get; set; }

        /// <summary>
        /// 关闭时间
        /// </summary>
        public DateTime CloseAt { get; set; }

        /// <summary>
        /// 总数量
        /// </summary>
        public int TotalQty { get; set; }

        /// <summary>
        /// 总箱数
        /// </summary>
        public int TotalCases { get; set; }

        /// <summary>
        /// 总重量
        /// </summary>
        public decimal TotalWeight { get; set; }

        /// <summary>
        /// 总体积
        /// </summary>
        public decimal TotalVolume { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; set; }



        public Guid WarehouseId { get; set; }

        public Guid? TenantId { get; set; }


        public ICollection<ReceiptDetail> ReceiptDetails { get; set; }
    }
}

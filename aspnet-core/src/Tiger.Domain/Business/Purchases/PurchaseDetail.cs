/**
 * 类    名：PurchaseOrderDetail   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/11 15:03:49       
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

namespace Tiger.Business.Purchases
{
    /// <summary>
    /// 采购单明细
    /// </summary>
    public class PurchaseDetail : FullAuditedAggregateRoot<Guid>, ISoftDelete, IMultiTenant
    {

        public string ProductSn { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 采购数量
        /// </summary>
        public int Qty { get; set; }

        /// <summary>
        /// 采购进价
        /// </summary>
        public decimal PurchasePrice { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; set; }

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


        [ForeignKey("WarehouseId")]
        public virtual Warehouse Warehouse { get; set; }

        [ForeignKey("ProductId")]
        public Guid ProductId { get; set; }

        public virtual Product Product { get; set; }

        public Guid? TenantId { get; set; }

        public Guid PurchaseHeaderId { get; set; }

        [ForeignKey("PurchaseHeaderId")]
        public virtual PurchaseHeader PurchaseHeader { get; set; }

        protected PurchaseDetail()
        {
        }

        public PurchaseDetail(
            Guid id,
            string productSn,
            string unit,
            int qty,
            decimal purchasePrice,
            string note,
            int totalQty,
            int openQty,
            string processStamp,
            Warehouse warehouse,
            Guid productId,
            Product product,
            Guid? tenantId
        ) : base(id)
        {
            ProductSn = productSn;
            Unit = unit;
            Qty = qty;
            PurchasePrice = purchasePrice;
            Note = note;
            TotalQty = totalQty;
            OpenQty = openQty;
            ProcessStamp = processStamp;
            Warehouse = warehouse;
            ProductId = productId;
            Product = product;
            TenantId = tenantId;
        }
    }
}

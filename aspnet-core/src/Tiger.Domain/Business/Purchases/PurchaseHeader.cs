/**
 * 类    名：PurchaseOrderHeader   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/11 15:02:54       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Tiger.Business.Purchases
{
    /// <summary>
    /// 采购进货单
    /// </summary>
    public class PurchaseHeader : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {   

        public Guid? TenantId { get; set; }

        public string WarehouseCode { get; set; }

        /// <summary>
        /// 单号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 采购金额
        /// </summary>
        public decimal PurchasePrice { get; set; }

        /// <summary>
        /// 采购单状态
        /// </summary>
        public int Status { get; set; }

        

        /// <summary>
        /// 总金额
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// 总数量
        /// </summary>
        public int TotalQty { get; set; }

        ///审核人
        public string AuditedBy { get; set; }


        /// <summary>
        /// 采购员
        /// </summary>
        public string PurchaseBy { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; set; }


        public Guid WarehouseId { get; set; }

        public Guid SupplyId { get; set; }

        public virtual ICollection<PurchaseDetail> PurchaseDetails { get; set; }



        protected PurchaseHeader()
        {
        }

        public PurchaseHeader(
            Guid id,
            Guid? tenantId,
            string warehouseCode,
            string code,
            decimal purchasePrice,
            int status,
            decimal totalAmount,
            int totalQty,
            string auditedBy,
            string purchaseBy,
            string note,
            Guid warehouseId,
            Guid supplyId,
            ICollection<PurchaseDetail> purchaseDetails
        ) : base(id)
        {
            TenantId = tenantId;
            WarehouseCode = warehouseCode;
            Code = code;
            PurchasePrice = purchasePrice;
            Status = status;
            TotalAmount = totalAmount;
            TotalQty = totalQty;
            AuditedBy = auditedBy;
            PurchaseBy = purchaseBy;
            Note = note;
            WarehouseId = warehouseId;
            SupplyId = supplyId;
            PurchaseDetails = purchaseDetails;
        }
    }
}

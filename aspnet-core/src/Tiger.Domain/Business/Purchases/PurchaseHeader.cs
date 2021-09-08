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
        /// 备注
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// 总金额
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// 总数量
        /// </summary>
        //public int TotalQty { get; set; }

        ///审核人
        public string AuditedBy { get; set; }


        /// <summary>
        /// 采购员
        /// </summary>
        public string PurchseBy { get; set; }


        public Guid WarehouseId { get; set; }

        public Guid SupplyId { get; set; }

        public virtual ICollection<PurchaseDetail> PurchaseOrderDetails { get; set; }


    }
}

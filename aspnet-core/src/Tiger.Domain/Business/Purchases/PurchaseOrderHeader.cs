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

namespace Tiger.Purchase
{
    /// <summary>
    /// 采购进货单
    /// </summary>
    public class PurchaseOrderHeader : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {   

        public Guid? TenantId { get; set; }

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

        public Guid WarehouseId { get; set; }

        public Guid SupplyId { get; set; }


    }
}

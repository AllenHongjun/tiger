/**
 * 类    名：PurchaseOrderDetail   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/11 15:03:49       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Tiger.Purchase
{
    /// <summary>
    /// 采购单明细
    /// </summary>
    public class PurchaseOrderDetail : FullAuditedAggregateRoot<Guid>, ISoftDelete, IMultiTenant
    {

        public string ProductSn { get; set; }

        public Guid ProductId { get; set; }

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

        public Guid? TenantId { get; set; }
    }
}

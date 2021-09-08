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
    /// 采购退款单明细
    /// </summary>
    public class PurchaseReturnDetail:FullAuditedAggregateRoot<Guid>, ISoftDelete, IMultiTenant
    {   
        /// <summary>
        /// 商品货号
        /// </summary>
        public string ProductSn { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }
        
        /// <summary>
        /// 采购进价
        /// </summary>
        public decimal PurchasePrice { get; set; }


        /// <summary>
        /// 退货总数量
        /// </summary>
        public int TotalQty { get; set; }

        /// <summary>
        /// 未退数量
        /// </summary>
        public int OpenQty { get; set; }


        /// <summary>
        /// 处理标记
        /// </summary>
        public string ProcessStamp { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; set; }


        [ForeignKey("WarehouseId")]
        public virtual Warehouse Warehouse { get; set; }

        [ForeignKey("ProductId")]
        public Guid ProductId { get; set; }

        public virtual Product Product { get; set; }

        public Guid? TenantId { get; set; }
    }
}

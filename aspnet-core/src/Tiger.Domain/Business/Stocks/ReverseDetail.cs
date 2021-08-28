/**
 * 类    名：ReverseOrderDetail   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/11 15:08:26       
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
    /// 拆套单明细(组装拆卸单)
    /// </summary>
    [Table("StockReverseDetail")]
    public class ReverseDetail : FullAuditedAggregateRoot<Guid>, ISoftDelete, IMultiTenant
    {

        public string ProductSn { get; set; }

        public string ProductName { get; set; }


        public string WarehouseCode { get; set; }


        [ForeignKey("WarehouseId")]
        public virtual Warehouse Warehouse { get; set; }


        public Guid ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }


        /// <summary>
        /// 拆套单id
        /// </summary>
        public Guid ReverseOrderId { get; set; }

        public Guid? TenantId { get; set; }

        
    }
}

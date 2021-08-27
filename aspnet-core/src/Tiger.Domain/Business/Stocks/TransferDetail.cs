/**
 * 类    名：TransferDetail   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/11 15:25:03       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Tiger.Stock
{
    /// <summary>
    /// 调拨单明细
    /// </summary>
    public class TransferDetail : FullAuditedAggregateRoot<Guid>, ISoftDelete, IMultiTenant
    {

        public string ProductSn { get; set; }

        public string ProductName { get; set; }

        /// <summary>
        /// 源仓库
        /// </summary>
        public Guid FromWarehouseId { get; set; }

        /// <summary>
        /// 总数量
        /// </summary>
        public int TotalQty { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string QuantityUnit { get; set; }

        /// <summary>
        /// 库存状态
        /// </summary>
        public int InventorySts { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 处理标记
        /// </summary>
        public string ProcessStamp { get; set; }

        /// <summary>
        /// 目标仓库
        /// </summary>
        public Guid ToWarehouseId { get; set; }

        public Guid ProductId { get; set; }

        /// <summary>
        /// 调拨单号
        /// </summary>
        public string TransferCode { get; set; }

        public Guid TransferId { get; set; }


        public Guid? TenantId { get; set; }
    }
}

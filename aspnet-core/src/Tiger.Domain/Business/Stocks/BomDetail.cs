/**
 * 类    名：BomDetail   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/11 15:22:14       
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
    /// 物料明细
    /// </summary>
    public class BomDetail : FullAuditedAggregateRoot<Guid>, ISoftDelete, IMultiTenant
    {

        public Guid BomId { get; set; }

        public string ProductSn { get; set; }

        public string ProductName { get; set; }

        public string WarehouseCode { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int BuildSequence { get; set; }

        /// <summary>
        /// 组套层次
        /// </summary>
        public int BuildLevel { get; set; }

        /// <summary>
        /// 每成品需要数量
        /// </summary>
        public int QtyNeededPerItem { get; set; }

        /// <summary>
        /// 数量单位
        /// </summary>
        public string QuantityUnit { get; set; }

        /// <summary>
        /// 分配规则
        /// </summary>
        public string AllocationRule { get; set; }

        /// <summary>
        /// 处理标记
        /// </summary>
        public string ProcessStamp { get; set; }




        public Guid? TenantId { get; set; }
    }
}

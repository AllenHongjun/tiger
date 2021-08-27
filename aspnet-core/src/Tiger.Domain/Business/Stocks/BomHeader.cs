/**
 * 类    名：BomHeader   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/11 15:21:44       
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
    /// 物料清单
    /// </summary>
    public class BomHeader : FullAuditedAggregateRoot<Guid>, ISoftDelete, IMultiTenant
    {




        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string QuantityUnit { get; set; }

        public string ProductName { get; set; }

        public string ProductSn { get; set; }

        /// <summary>
        /// 处理标记
        /// </summary>
        public string ProcessStamp { get; set; }

        public string WarehouseCode { get; set; }

        public Guid WarehouseId { get; set; }

        public Guid? TenantId { get; set; }
    }
}

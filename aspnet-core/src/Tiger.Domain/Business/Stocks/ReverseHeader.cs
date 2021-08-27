/**
 * 类    名：ReverseOrderHeader   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/11 15:07:35       
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
    /// 拆套单(组装拆卸单)
    /// </summary>
    public class ReverseHeader : FullAuditedAggregateRoot<Guid>, ISoftDelete, IMultiTenant
    {

        public string ProductSn { get; set; }

        public Guid ProductId { get; set; }

        public string ProductName { get; set; }

        public int InventorySts { get; set; }

        /// <summary>
        /// 计划数量
        /// </summary>
        public int TotalQty { get; set; }

        /// <summary>
        /// 可用加工数量
        /// </summary>
        public int OpenQty { get; set; }

        /// <summary>
        /// 以完成数量
        /// </summary>
        public int BuildQty { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string QuantityUnit { get; set; }

        /// <summary>
        /// 释放时间
        /// </summary>
        public DateTime ReleaseAt { get; set; }

        /// <summary>
        /// 完成时间
        /// </summary>
        public DateTime CompleteAt { get; set; }

        // 使用bom套件

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }


        public Guid WarehouseCode { get; set; }

        public string Code { get; set; }

        public Guid? TenantId { get; set; }
    }
}

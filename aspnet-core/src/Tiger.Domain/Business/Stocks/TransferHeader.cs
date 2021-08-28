/**
 * 类    名：TransferHeader   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/11 15:24:31       
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
    /// 调拨(移库)单
    /// </summary>
    public class TransferHeader : FullAuditedAggregateRoot<Guid>, ISoftDelete, IMultiTenant
    {
        /// <summary>
        /// 调拨单号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime BeginTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        public DateTime CloasAt { get; set; }

        public string CloseBy { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; set; }


        /// <summary>
        /// 总数量
        /// </summary>
        public int TotalQty { get; set; }

        /// <summary>
        /// 总重量
        /// </summary>
        public int TotalWeight { get; set; }

        /// <summary>
        /// 总体积
        /// </summary>
        public int TotalVolume { get; set; }

        public Guid? TenantId { get; set; }

        public virtual ICollection<TransferDetail> TransferDetails { get; set; }

    }
}

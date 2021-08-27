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

        public DateTime BeginTime { get; set; }

        public DateTime EndTime { get; set; }

        public DateTime CloasAt { get; set; }

        public string CloseBy { get; set; }

        public Guid? TenantId { get; set; }

        public virtual ICollection<TransferHeader> TransferHeaders { get; set; }

    }
}

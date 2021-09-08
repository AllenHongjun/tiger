/**
 * 类    名：CheckHeader   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/11 15:23:21       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Tiger.Stock
{
    /// <summary>
    /// 盘点单头
    /// </summary>
    [Table("StockCheckHeader")]
    public class CheckHeader : FullAuditedAggregateRoot<Guid>, ISoftDelete, IMultiTenant
    {
        /// <summary>
        /// 仓库编码
        /// </summary>
        public string WarehouseCode { get; set; }

        /// <summary>
        /// 盘点类型 0->定期盘点; 1->重点盘点;  2->循环盘点
        /// </summary>
        public int CheckType { get; set; }

        /// <summary>
        /// 盘点状态 0-> 未开始; 1-> 盘点中; 2-> 已完成; 3-> 已关闭
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// 关闭人
        /// </summary>
        public string CloseBy { get; set; }

        /// <summary>
        /// 关闭时间
        /// </summary>
        public DateTime CloseAt { get; set; }

        /// <summary>
        /// 处理标记
        /// </summary>
        public string ProcessStamp { get; set; }




        /// <summary>
        /// 盘点日期
        /// </summary>
        public DateTime CheckDate { get; set; }

        /// <summary>
        /// 单号
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 盘点仓库
        /// </summary>
        public Guid WarehouseId { get; set; }




        public Guid? TenantId { get; set; }

        public virtual ICollection<CheckDetail> CheckDetails { get; set; } 
    }
}

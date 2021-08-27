/**
 * 类    名：Supply   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/11 13:23:55       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Tiger.Basic
{
    /// <summary>
    /// 供应商
    /// </summary>
    public class Supply : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string AttentionTo { get; set; }

        /// <summary>
        /// 邮编
        /// </summary>
        public string PostCode { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 区
        /// </summary>
        public string Reginon { get; set; }

        /// <summary>
        /// 详细地址街道
        /// </summary>
        public string Address { get; set; }

        public string Phone { get; set; }

        /// <summary>
        /// 0->禁用; 1->启用
        /// </summary>
        public int Status { get; set; }


        public Guid? WarehouseId { get; set; }


        public Guid? TenantId { get; set; }
    }
}

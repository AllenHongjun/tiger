/**
 * 类    名：Store   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/11 13:25:06       
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
    /// 店铺
    /// </summary>
    public class Store:FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        /// <summary>
        /// 店铺编码
        /// </summary>
        public string Code { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        /// <summary>
        /// 状态 0->禁用; 1->启用;
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 销售区域
        /// </summary>
        public string Apolygons { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public string Lat { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public string Lng { get; set; }

        public Guid WarehouseId { get; set; }

        public Guid? TenantId { get; set; }


    }
}

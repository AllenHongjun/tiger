/**
 * 类    名：Warehouse   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/11 13:14:18       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Tiger.Basic
{
    /// <summary>
    /// 仓库
    /// </summary>
    [Table("AppWarehouses")]
    public class Warehouse : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        
        /// <summary>
        /// 仓库编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 0 禁用; 1启用
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 0->总仓; 1-> 分仓
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 经纬
        /// </summary>
        public double Lat { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public double Lng { get; set; }

        public string Address { get; set; }

        /// <summary>
        /// 区
        /// </summary>
        public string District { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        public string City { get; set; }

        public string Province { get; set; }

        /// <summary>
        /// 邮编
        /// </summary>
        public string PosttalCode { get; set; }

        public string Mobile { get; set; }

        public Guid? TenantId { get; set; }

        /// <summary>
        /// 组织id
        /// </summary>
        public Guid OrgId { get; set; }





    }
}

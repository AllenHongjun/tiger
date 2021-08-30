/**
 * 类    名：AttributeCategory   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/11 13:54:41       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Tiger.Basic
{
    /// <summary>
    /// 商品属性类型表
    /// 
    /// 商品属性分规格和参数 规格用户用户购买商品是选择  参数用户标注商品属性以及搜索是筛选
    /// </summary>
    [Table("AppProductAttributeTypes")]
    public class ProductAttributeType: AuditedAggregateRoot<Guid>,IMultiTenant
    {
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        //[Column(TypeName = "varchar(200)")]
        [MaxLength(200)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 属性数量
        /// </summary>
        public int AttributeCount { get; set; }

        /// <summary>
        /// 参数数量
        /// </summary>
        public int ParamCount { get; set; }

        
    }
}

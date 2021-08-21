/**
 * 类    名：Product   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/11 13:53:14       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tiger.Basic.Products;
using Tiger.Business;
using Tiger.Business.Basic;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Tiger.Basic
{   
    /// <summary>
    /// 商品表
    /// </summary>
    public class Product : FullAuditedAggregateRoot<Guid>,  ISoftDelete, IMultiTenant
    {
        //public int BrandId { get; set; }

        //public int ProductCategoryId { get; set; }

        //public virtual Category Category { get; set; }

        public String Name { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string Picture { get; set; }

        /// <summary>
        /// 货号
        /// </summary>
        public string ProductSn { get; set; }

        /// <summary>
        /// 发布状态
        /// </summary>
        public ProductPublishStatus PublishStatus { get; set; }

        //public int DeleteStatus { get; set; }

        public int Sort { get; set; }

        public decimal Price { get; set; }

        public String SubTitle { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 详情描述
        /// </summary>
        public string DetailDesc { get; set; }

        //[NotMapped]
        //public virtual ICollection<ProductTag> ProductTags { get; set; }

        public Guid? TenantId { get; set; }

        //public virtual ICollection<Order> Orders { get; set; }




    }
}

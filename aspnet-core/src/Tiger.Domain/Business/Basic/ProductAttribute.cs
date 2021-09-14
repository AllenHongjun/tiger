/**
 * 类    名：Attribute   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/11 13:15:28       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tiger.Business.Basic;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Tiger.Basic
{
    /// <summary>
    /// 商品属性表
    /// 比如 颜色 容量 重量
    /// 如果对应的参数是规格且规格支持手动添加，那么该表用于存储手动新增的值；如果对应的商品属性是参数，那么该表用于存储参数的值。
    /// </summary>
    [Table("ProductAttribute")]
    //[Comment("商品属性表")]
    public class ProductAttribute: AuditedAggregateRoot<Guid>, ISoftDelete, IMultiTenant
    {
        public Guid? TenantId { get; set; }
        //[Column("Name")]
        [Column(TypeName = "varchar(200)")]
        [MaxLength(500)]
        [Required]
        public string Name { get; set; }

        

        /// <summary>
        /// 属性选择类型：0->唯一；1->单选；2->多选；对应属性和参数意义不同；
        /// </summary>
        public int SelectType { get; set; }

        /// <summary>
        /// 属性录入方式：0->手工录入；1->从列表中选取
        /// </summary>
        public int InputType { get; set; }

        /// <summary>
        /// 可选值列表，以逗号隔开
        /// </summary>
        public string InputList { get; set; }

        /// <summary>
        /// 排序字段：最高的可以单独上传图片
        /// </summary>
        public int Sort { get; set; }
        
        [Description("分类筛选样式：1->普通；1->颜色")]
        public int FilterType { get; set; }

        /// <summary>
        /// 检索类型；0->不需要进行检索；1->关键字检索；2->范围检索',
        /// </summary>
        public int SearchType { get; set; }

        /// <summary>
        /// '相同属性产品是否关联；0->不关联；1->关联',
        /// </summary>
        [NotMapped]
        public int RelatedStatus { get; set; }

        /// <summary>
        /// '是否支持手动新增；0->不支持；1->支持',
        /// </summary>
        public int HandAddStatus { get; set; }

        /// <summary>
        /// '属性的类型；0->规格；1->参数',
        /// </summary>
        public int Type { get; set; }
        public bool IsDeleted { get ; set ; }

        public Guid ProductAttributeTypeId { get; set; }

        [ForeignKey("ProductAttributeTypeId")]
        public ProductAttributeType ProductAttributeType { get; set; }

        public virtual ICollection<ProductAttributeValue> ProductAttributeValues { get; set; }

        //public virtual ICollection<Product> Products { get; set; }

        
    }
}

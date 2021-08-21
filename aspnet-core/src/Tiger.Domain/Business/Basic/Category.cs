/**
 * 类    名：Category   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/11 13:13:19       
 * 说    明: 
 * 
 */

using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace Tiger.Basic
{
    /// <summary>
    /// 产品分类
    /// </summary>
    public class Category:AggregateRoot<Guid>, IMultiTenant
    {
        //public Category()
        //{
        //}

        //public Category(Guid parentId, Category parentCategory, string name, int level, int productCount, int showStatus, int sort, string icon, string keyword, string description, ICollection<Product> products)
        //{
        //    ParentId = parentId;
        //    ParentCategory = parentCategory ?? throw new ArgumentNullException(nameof(parentCategory));
        //    Name = name ?? throw new ArgumentNullException(nameof(name));
        //    Level = level;
        //    ProductCount = productCount;
        //    ShowStatus = showStatus;
        //    Sort = sort;
        //    Icon = icon ?? throw new ArgumentNullException(nameof(icon));
        //    Keyword = keyword ?? throw new ArgumentNullException(nameof(keyword));
        //    Description = description ?? throw new ArgumentNullException(nameof(description));


        //    Products = new Collection<Product>();
        //}

        [CanBeNull]
        public Guid ParentId { get; set; }

        public virtual Category ParentCategory { get; set; }

        public string Name { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        public int Level { get; set; }

        [CanBeNull]
        public int ProductCount { get; set; }

        /// <summary>
        /// 显示状态 0->不显示 1->显示
        /// </summary>
        public int ShowStatus { get; set; }

        public int Sort { get; set; }

        public string Icon { get; set; }

        /// <summary>
        /// 关键字
        /// </summary>
        [CanBeNull]
        public string Keyword { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        public Guid? TenantId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tiger.Business.Demo;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Tiger.Books
{

    /// <summary>
    /// 书籍实体
    /// </summary>
    /// <remarks>
    /// 1.AuditedAggregateRoot类在AggregateRoot类的基础上添加了一些审计属性(CreationTime, CreatorId, LastModificationTime 等).
    /// 目前一本书 值关联一个作者 一个作者可以关联多本书籍
    /// 2. ABP为实体提供了两个基本的基类: AggregateRoot和Entity. Aggregate Root是领域驱动设计 概念之一. 可以视为直接查询和处理的根实体(请参阅实体文档).
    /// </remarks>
    public class Book:AuditedEntity<Guid>, ISoftDelete, IMultiTenant
    {
        //如果使用带参数的构造函数创建实体,那么还要创建一个 private 或 protected 构造函数. 当数据库提供程序从数据库读取你的实体时(反序列化时)将使用它.
        public Book()
        {
        }

        public Book(Guid id, String name, BookType type,DateTime publishDate, float price) :base(id)
        {
            Id = id;
            Name = name;
            Type = type;
            PublishDate = publishDate;
            Price = price;
        }

        public string Name { get; set; }

        public BookType Type { get; set; }

        public DateTime PublishDate { get; set; }

        public float Price { get; set; }

        // 将实体标记为已删除,并不是物理删除. 实现 ISoftDelete 接口将你的实体"软删除".
        public bool IsDeleted { get; set; } //Defined by ISoftDelete

        //  多租户应用程序通常需要在租户间隔离数据. 实现 IMultiTenant 接口使你的实体支持 "多租户".

        public Guid? TenantId { get; set; } //Defined by IMultiTenant

        /// <summary>
        /// In this tutorial, we preferred to not add a navigation property to the Author entity from the Book class (like public Author Author { get; set; }). This is due to follow the DDD best practices (rule: refer to other aggregates only by id).
        /// </summary>
        public Guid AuthorId { get; set; }

        /// <summary>
        /// 作者对象
        /// </summary>
        /// <remarks>
        /// 导航属性
        /// </remarks>
        [ForeignKey("AuthorId")]
        public virtual Author Author { get; set; }

    }
}

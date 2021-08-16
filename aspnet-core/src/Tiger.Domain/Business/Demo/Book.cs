using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tiger.Books
{

    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// 
    /// 1.AuditedAggregateRoot类在AggregateRoot类的基础上添加了一些审计属性(CreationTime, CreatorId, LastModificationTime 等).
    /// </remarks>
    public class Book:AuditedAggregateRoot<Guid>
    {
        //如果使用带参数的构造函数创建实体,那么还要创建一个 private 或 protected 构造函数. 当数据库提供程序从数据库读取你的实体时(反序列化时)将使用它.
        public Book()
        {
        }

        public Book(Guid id):base(id)
        {
        }

        public string Name { get; set; }

        public BookType Type { get; set; }

        public DateTime PublishDate { get; set; }

        public float Price { get; set; }
    }
}

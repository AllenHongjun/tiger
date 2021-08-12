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
        public string Name { get; set; }

        public BookType Type { get; set; }

        public DateTime PublishDate { get; set; }

        public float Price { get; set; }
    }
}

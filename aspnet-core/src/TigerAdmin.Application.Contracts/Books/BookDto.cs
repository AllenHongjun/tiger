using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace TigerAdmin.Books
{

    /// <summary>
    /// 
    /// 数据传输对象(DTO)用于在应用层和表示层或其他类型的客户端之间传输数据.
    /// 数据传输层对象使用说明 https://docs.abp.io/zh-Hans/abp/latest/Data-Transfer-Objects
    /// </summary>
    public class BookDto:AuditedEntityDto<Guid>
    {
        public string Name { get; set; }

        public BookType Type { get; set; }

        public DateTime PublishDate { get; set; }

        public float Price { get; set; }
    }
}

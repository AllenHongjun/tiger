using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Tiger.Books
{

    /// <summary>
    /// 书籍
    /// </summary>
    /// <remarks>
    /// 数据传输对象(DTO)用于在应用层和表示层或其他类型的客户端之间传输数据.
    /// 数据传输层对象使用说明 https://docs.abp.io/zh-Hans/abp/latest/Data-Transfer-Objects
    /// </remarks>
    public class BookDto:AuditedEntityDto<Guid>
    {
        public string Name { get; set; }

        public BookType Type { get; set; }

        public DateTime PublishDate { get; set; }

        public float Price { get; set; }

        /// <summary>
        ///  dto 对象里面 需要把 autherId  和 author name 属性全部都加上去
        /// </summary>
        public Guid AuthorId { get; set; }

        /// <summary>
        /// 作者名字
        /// </summary>
        /// <remarks>
        /// 通过join 关联查询数据
        /// </remarks>

        public string AuthorName { get; set; }
    }
}

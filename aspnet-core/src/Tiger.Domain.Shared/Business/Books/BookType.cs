using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Books
{

    /// <summary>
    /// 书籍类型
    /// </summary>
    /// <remarks>
    /// 上面所用到了 BookType 枚举,在 Acme.BookStore.Domain.Shared 项目创建 BookType.
    /// </remarks>
    public enum BookType
    {
        Undefined,
        Adventure,

        /// <summary>
        /// 传记
        /// </summary>
        Biography,
        Dystopia,
        Fantastic,

        /// <summary>
        /// 恐怖
        /// </summary>
        Horror,
        Science,
        ScienceFiction,

        /// <summary>
        /// 诗歌
        /// </summary>
        Poetry
           
    }
}

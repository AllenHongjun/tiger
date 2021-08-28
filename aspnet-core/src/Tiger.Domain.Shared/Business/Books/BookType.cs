using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Books
{

    /// <summary>
    /// 
    /// 
    /// 2.上面所用到了 BookType 枚举,在 Acme.BookStore.Domain.Shared 项目创建 BookType.
    /// </summary>
    public enum BookType
    {
        Undefined,
        Adventure,
        Biography,
        Dystopia,
        Fantastic,
        Horror,
        Science,
        ScienceFiction,
        Poetry
           
    }
}

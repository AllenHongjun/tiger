/**
 * 类    名：IBookRepository   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/16 14:10:40       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Books;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Business.Demo
{   
    /// <summary>
    /// 添加图书管理 自定义的仓储层
    /// </summary>
    public interface IBookRepository : IRepository<Book, Guid>
    {   
        /// <summary>
        /// 根据图书类型删除书籍
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        Task DeleteBooksByType(BookType type);
    }
}

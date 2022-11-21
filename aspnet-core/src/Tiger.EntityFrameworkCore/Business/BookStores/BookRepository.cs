/**
 * 类    名：BookRepository   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/16 14:13:25       
 * 说    明: 
 * 
 */

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tiger.Books;
using Tiger.Business.Demo;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Demo
{   
    /// <summary>
    /// 书籍仓储层的实现
    /// </summary>
    public class BookRepository : EfCoreRepository<TigerDbContext, Book, Guid>, IBookRepository
    {
        public BookRepository(IDbContextProvider<TigerDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }


        /// <summary>
        /// 根据id删除一本书籍
        /// </summary>
        /// <param name="id"></param>
        /// <param name="autoSave"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <remarks>
        /// 覆盖默认仓储方法对其自定义
        /// </remarks>
        public async override Task DeleteAsync(
            Guid id,
            bool autoSave = false,
            CancellationToken cancellationToken = default)
        {
            //Custom implementation of the delete method
            var entity = await FindAsync(id, cancellationToken: cancellationToken);
            if (entity == null)
            {
                return;
            }

            await DeleteAsync(entity, autoSave, cancellationToken);
        }



        /// <summary>
        /// 根据类型删除一本书籍
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public async Task DeleteBooksByType(BookType type)
        {

            // 直接指向sql 语句的代码
            await DbContext.Database.ExecuteSqlRawAsync(
                $"DELETE FROM Books WHERE Type = {(int)type}"
            );
        }
    }
}

/**
 * 类    名：BookManager   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/16 14:02:49       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Books;
using Volo.Abp.Domain.Repositories;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Tiger.Business.Demo
{
    /// <summary>
    /// 在领域服务中创建一个新的Book实例并且使用仓储持久化到数据库中
    /// Manager 属于 领域服务的方法
    /// </summary>
    public class BookManager:DomainService
    {
        private readonly IRepository<Book, Guid> _bookRepository;

        public BookManager(IRepository<Book, Guid> bookRepository) //注入默认仓储
        {
            _bookRepository = bookRepository;
        }

        public async Task<Book> CreateBook(string name, BookType type)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var book = new Book
            {
                //Id = GuidGenerator.Create(),
                Name = name,
                Type = type
            };

            await _bookRepository.InsertAsync(book); //使用仓储提供的标准方法

            // 创建修改成功 返回实体类。 这样就可以判断是否添加修改成功。
            return book;
        }
    }
}

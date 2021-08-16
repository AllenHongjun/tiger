/**
 * 类    名：BookStoreDataSeedContributor   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/16 13:37:21       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Books;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;

namespace Tiger.Business.Demo
{
    /// <summary>
    /// 使用数据库的某些应用程序(或模块),可能需要有一些初始数据才能​​够正常启动和运行. 例如管理员用户和角色必须在一开始就可用. 
    /// </summary>
    public class BookStoreDataSeedContributor
        : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Book, Guid> _bookRepository;
        private readonly IGuidGenerator _guidGenerator;
        private readonly ICurrentTenant _currentTenant;

        public BookStoreDataSeedContributor(
            IRepository<Book, Guid> bookRepository,
            IGuidGenerator guidGenerator,
            ICurrentTenant currentTenant)
        {
            _bookRepository = bookRepository;
            _guidGenerator = guidGenerator;
            _currentTenant = currentTenant;
        }

        /// <summary>
        /// 如果没有图书,则向数据库播种一个初始图书
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task SeedAsync(DataSeedContext context)
        {
            using (_currentTenant.Change(context?.TenantId))
            {   
                // 如果数据表里面没有数据就会插入一条数据
                if (await _bookRepository.GetCountAsync() > 0)
                {
                    return;
                }

                var book = new Book(
                    id: _guidGenerator.Create(),
                    name: "The Hitchhiker's Guide to the Galaxy",
                    type: BookType.ScienceFiction,
                    publishDate: new DateTime(1979, 10, 12),
                    price: 42
                );

                await _bookRepository.InsertAsync(book);
            }
        }
    }
}

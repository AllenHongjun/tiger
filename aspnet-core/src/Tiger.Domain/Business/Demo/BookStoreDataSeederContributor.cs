/**
 * 类    名：BookStoreDataSeederContributor   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/17 1:40:04       
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

namespace Tiger.Business.Demo
{

    /// 
    /// <summary>
    ///  添加种子数据
    /// </summary>
    /// <remarks>
    /// 使用数据库的某些应用程序(或模块),可能需要有一些初始数据才能​​够正常启动和运行. 例如管理员用户和角色必须在一开始就可用.
    /// </remarks>
    public class BookStoreDataSeederContributor
        : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Book, Guid> _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly AuthorManager _authorManager;

        public BookStoreDataSeederContributor(
            IRepository<Book, Guid> bookRepository,
            IAuthorRepository authorRepository,
            AuthorManager authorManager)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _authorManager = authorManager;
        }

        /// <summary>
        /// 添加租户的时候会调用这个里面的方法
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task SeedAsync(DataSeedContext context)
        {

            return;

            // 这个 Seed的代码坑了自己。。注意调试。。有些方法实现 转不过去。。 把源代码引入就好了。

            //if (await _bookRepository.GetCountAsync() > 0)
            //{
            //    return;
            //}

            //var orwell = await _authorRepository.InsertAsync(
            //    await _authorManager.CreateAsync(
            //        "George Orwell",
            //        new DateTime(1903, 06, 25),
            //        "Orwell produced literary criticism and poetry, fiction and polemical journalism; and is best known for the allegorical novella Animal Farm (1945) and the dystopian novel Nineteen Eighty-Four (1949)."
            //    )
            //);

            //var douglas = await _authorRepository.InsertAsync(
            //    await _authorManager.CreateAsync(
            //        "Douglas Adams",
            //        new DateTime(1952, 03, 11),
            //        "Douglas Adams was an English author, screenwriter, essayist, humorist, satirist and dramatist. Adams was an advocate for environmentalism and conservation, a lover of fast cars, technological innovation and the Apple Macintosh, and a self-proclaimed 'radical atheist'."
            //    )
            //);

            //await _bookRepository.InsertAsync(
            //    new Book
            //    {   
            //        // 为书籍关联作者
            //        AuthorId = orwell.Id, // SET THE AUTHOR
            //        Name = "1984",
            //        Type = BookType.Dystopia,
            //        PublishDate = new DateTime(1949, 6, 8),
            //        Price = 19.84f
            //    },
            //    autoSave: true
            //);

            //await _bookRepository.InsertAsync(
            //    new Book
            //    {
            //        AuthorId = douglas.Id, // SET THE AUTHOR
            //        Name = "The Hitchhiker's Guide to the Galaxy",
            //        Type = BookType.ScienceFiction,
            //        PublishDate = new DateTime(1995, 9, 27),
            //        Price = 42.0f
            //    },
            //    autoSave: true
            //);
        }
    }
}

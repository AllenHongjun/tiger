/**
 * 类    名：AuthorManager   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/17 1:00:16       
 * 说    明: 
 * 
 */

using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.Guids;
using Volo.Abp;
using Tiger.Business.Orders;
using System.Linq;

namespace Tiger.Business.Demo
{
    /// <summary>
    /// AuthorManager forces to create an author and change name of an author in a controlled way
    /// </summary>
    public class AuthorManager : DomainService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorManager(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        /// <summary>
        /// 添加作者
        /// </summary>
        /// <param name="name"></param>
        /// <param name="birthDate"></param>
        /// <param name="shortBio"></param>
        /// <returns></returns>
        /// <exception cref="AuthorAlreadyExistsException"></exception>
        public async Task<Author> CreateAsync(
            [NotNull] string name,
            DateTime birthDate,
            [CanBeNull] string shortBio = null)

        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var existingAuthor = await _authorRepository.FindByNameAsync(name);
            if (existingAuthor != null)
            {
                throw new AuthorAlreadyExistsException(name);
            }

            return new Author(
                GuidGenerator.Create(),
                name,
                birthDate,
                shortBio
            );
        }

        /// <summary>
        /// 修改作者名称
        /// </summary>
        /// <param name="author"></param>
        /// <param name="newName"></param>
        /// <returns></returns>
        /// <exception cref="AuthorAlreadyExistsException"></exception>
        public async Task ChangeNameAsync(
            [NotNull] Author author,
            [NotNull] string newName)
        {
            Check.NotNull(author, nameof(author));
            Check.NotNullOrWhiteSpace(newName, nameof(newName));

            var existingAuthor = await _authorRepository.FindByNameAsync(newName);
            if (existingAuthor != null && existingAuthor.Id != author.Id)
            {
                throw new AuthorAlreadyExistsException(newName);
            }

            author.ChangeName(newName);
        }


        /// <summary>
        /// 获取一个包含 BooksId 的 author 对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task TestWithDetails(Guid id)
        {
            // 预先加载 / 包含子对象的加载

            //通过包含子集合获取一个 IQueryable<T>
            var queryable = _authorRepository.WithDetails(x => x.Books);

            //应用其他的 LINQ 扩展方法
            var query = queryable.Where(x => x.Id == id);

            //执行此查询并获取结果  AsyncExecuter 用于执行异步 LINQ 扩展,而无需依赖 EF Core
            var author = await AsyncExecuter.FirstOrDefaultAsync(query);
        }

        /// <summary>
        /// 获取一个包含 Books 的 authors 列表
        /// </summary>
        /// <returns></returns>
        public async Task TestWithDetails()
        {
            //通过包含一个子集合获取一个 IQueryable<T>
            var queryable =  _authorRepository.WithDetails(x => x.Books);

            //执行此查询并获取结果
            var authos = await AsyncExecuter.ToListAsync(queryable);
        }

    }
}

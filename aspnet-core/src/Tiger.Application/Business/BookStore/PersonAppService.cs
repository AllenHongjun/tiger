/**
 * 类    名：PersonAppService   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/16 12:47:21       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Tiger.Business.Demo;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Linq;

namespace Tiger.Books.Demo
{   
    /// <summary>
    /// 人物
    /// </summary>
    /// <remark>
    /// repssitories 仓储数据查询
    /// </remark>
    [RemoteService(false)]
    public class PersonAppService : ApplicationService
    {
        //ABP为每个聚合根或实体提供了 默认的通用(泛型)仓储 . 你可以在服务中注入 IRepository<TEntity, TKey> 使用标准的CRUD操作.
        private readonly IRepository<Person, Guid> _personRepository;

        private readonly IAsyncQueryableExecuter _asyncExecuter;

        //PersonAppService 在它的构造函数中注入了 IRepository<Person, Guid> .
        public PersonAppService(IRepository<Person, Guid> personRepository, IAsyncQueryableExecuter asyncExecuter)
        {
            _personRepository = personRepository;
            _asyncExecuter=asyncExecuter;
        }

        //Create 方法使用了 InsertAsync 创建并保存新的实体.
        public async Task Create(CreatePersonDto input)
        {
            var person = new Person { Name = input.Name, Age = input.Age };

            await _personRepository.InsertAsync(person);
        }


        //
        /// <summary>
        /// 获取列表数据
        /// </summary>
        /// <param name="nameFilter"></param>
        /// <returns></returns>
        /// <remarks>
        /// GetList 方法使用标准LINQ Where 和 ToList 方法在数据源中过滤并获取People集合.
        /// </remarks>
        public List<PersonDto> GetList(string nameFilter)
        {   
            // 主键 始终使用这个来创建。 可以可以保证连续性
            GuidGenerator.Create();
            var people = _personRepository
                .Where(p => p.Name.Contains(nameFilter))
                .OrderBy(p => p.Name)
                .ToList();

            return people
                .Select(p => new PersonDto { Id = p.Id, Name = p.Name, Age = p.Age })
                .ToList();
        }


        /// <summary>
        /// 通过仓储对象来对实体执行LINQ查询以操作数据库
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<List<PersonDto>> GetListAsync(string filter)
        {

            // 创建一个查询
            var query = from person in _personRepository
                        where person.Name == filter
                        orderby person.Name
                        select person;

            // 执行查询
            var people = await query.ToDynamicListAsync();

            // 转DTO并返回给客户端
            return people.Select(p => new PersonDto { Name = p.Name }).ToList();
        }



        /// <summary>
        /// 测试 IQueryable 异步操作
        /// </summary>
        /// <returns></returns>
        public async Task TestQueryableAndAsync(string name)
        {

            //ABP 的 应用服务层没有直接引用 EFcore 
            // 强烈建议使用异步方法! 在执行数据库查询时不要使用同步LINQ方法,以便能够开发可伸缩的应用程序
            // 选项-1: 引用EF Core


            // 选项-2: 使用IRepository异步扩展方法
            var countAll = await _personRepository
            .GetCountAsync();

            var count = await _personRepository
                .GetCountAsync();

            //var book1984 = await _personRepository
            //    .FirstAsync(x => x.Name == "John");



            // 选项-3: IAsyncQueryableExecuter
            //Create the query
            var query = _personRepository
                .Where(p => p.Name.Contains(name))
                .OrderBy(p => p.Name);

            //Run the query asynchronously

            // `IAsyncQueryableExecuter` 是一个用于异步执行 `IQueryable<T>` 对象的服务,**不依赖于实际的数据库提供程序**.
            List<Person> products = await _asyncExecuter.ToListAsync(query);

            // `ApplicationService` 和 `DomainService` 基类已经预属性注入了 `AsyncExecuter` 属性,所以你可直接使用.

            //...

        }

    }

}

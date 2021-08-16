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
using System.Text;
using System.Threading.Tasks;
using Tiger.Business.Demo;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Books.Demo
{
    public class PersonAppService : ApplicationService
    {
        //ABP为每个聚合根或实体提供了 默认的通用(泛型)仓储 . 你可以在服务中注入 IRepository<TEntity, TKey> 使用标准的CRUD操作.
        private readonly IRepository<Person, Guid> _personRepository;

        //PersonAppService 在它的构造函数中注入了 IRepository<Person, Guid> .
        public PersonAppService(IRepository<Person, Guid> personRepository)
        {
            _personRepository = personRepository;
        }

        //Create 方法使用了 InsertAsync 创建并保存新的实体.
        public async Task Create(CreatePersonDto input)
        {
            var person = new Person { Name = input.Name, Age = input.Age };

            await _personRepository.InsertAsync(person);
        }

        //GetList 方法使用标准LINQ Where 和 ToList 方法在数据源中过滤并获取People集合.
        public List<PersonDto> GetList(string nameFilter)
        {
            var people = _personRepository
                .Where(p => p.Name.Contains(nameFilter))
                .ToList();

            return people
                .Select(p => new PersonDto { Id = p.Id, Name = p.Name, Age = p.Age })
                .ToList();
        }
    }

}

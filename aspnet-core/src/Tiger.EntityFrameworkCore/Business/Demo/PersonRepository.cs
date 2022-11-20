/**
 * 类    名：PersonRepository   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/16 12:58:13       
 * 说    明: 
 * 
 */

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger.Business.Demo;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Demo
{
    /// <summary>
    /// 人物
    /// </summary>
    /// <remarks>
    /// 自定义存储库依赖于你使用的数据访问工具. 在此示例中, 我们将使用Entity Framework Core:
    /// 通过接口的定义的好处 如果 数据仓储层 可以有  mysql sqlserver mongodb 各种实现。但是只要定义一套接口就可以使用了。
    /// </remarks>
    public class PersonRepository : EfCoreRepository<TigerDbContext, Person, Guid>, IPersonRepository
    {
        public PersonRepository(IDbContextProvider<TigerDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        /// <summary>
        /// 可以直接使用数据库访问提供程序 (本例中是 DbContext ) 来执行操作.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public async Task<Person> FindByNameAsync(string name)
        {   
            // 或者使用DbSet变量来处理
            return await DbSet // DbContext.Set<Person>()
                .Where(p => p.Name == name)
                .FirstOrDefaultAsync();
        }

        
       
    }

}

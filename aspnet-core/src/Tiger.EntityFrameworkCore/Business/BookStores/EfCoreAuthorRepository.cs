/**
 * 类    名：EfCoreAuthorRepository   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/17 1:16:31       
 * 说    明: 
 * 
 */

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
//using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tiger.Business.Demo;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Demo
{
    /// <summary>
    /// 实现IAuthorRepository接口的仓储
    /// </summary>
    /// <remarks>
    /// 继承自 `EfCoreRepository`, 所以继承了标准repository的方法实现.
    /// </remarks>
    public class EfCoreAuthorRepository
        : EfCoreRepository<TigerDbContext, Author, Guid>,
            IAuthorRepository
    {
        public EfCoreAuthorRepository(
            IDbContextProvider<TigerDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        

        public async Task<Author> FindByNameAsync(string name)
        {
            return await DbSet
                .OrderBy(x => x.Id)
                .FirstOrDefaultAsync(author => author.Name == name);
        }

        /// <summary>
        /// 根据名称查找作者
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <remarks>
        /// CancellationToken https://zhuanlan.zhihu.com/p/357602454
        /// </remarks>
        public async Task<Author> FindByNameAsync(
            string name,
            bool includeDetails = true,
            CancellationToken cancellationToken = default)
        {
            return await DbSet
                .OrderBy(x => x.Id)
                .FirstOrDefaultAsync(author => author.Name == name, GetCancellationToken(cancellationToken));
        }

        /// <summary>
        /// 分页获取作者列表
        /// </summary>
        /// <param name="skipCount"></param>
        /// <param name="maxResultCount"></param>
        /// <param name="sorting">`sorting` 可以是一个字符串, 如 `Name`, `Name ASC` 或 `Name DESC`</param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<List<Author>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null)
        {
            return await DbSet
                // `WhereIf` 是ABP 框架的快捷扩展方法. 它仅当第一个条件满足时, 执行 `Where` 查询. (根据名字查询, 仅当 filter 不为空).
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    author => author.Name.Contains(filter)
                 )
                .OrderBy(sorting => sorting.BirthDate)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }


        /// <summary>
        /// 获取总数量
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<long> GetCountAsync(string filter = null, CancellationToken cancellationToken = default)
        {
            return await DbSet
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    u =>
                        u.Name.Contains(filter)
                ).CountAsync(cancellationToken: GetCancellationToken(cancellationToken));
        }


       

    }
}

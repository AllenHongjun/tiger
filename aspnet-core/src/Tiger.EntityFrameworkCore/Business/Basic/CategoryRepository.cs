using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger.Business.Basic;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Basic
{
    public class CategoryRepository
        : EfCoreRepository<TigerDbContext, Category, Guid>,
            ICategoryRepository
    {
        public CategoryRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<List<Category>> GetChildrenAsync(Guid parentId)
        {
            return await DbSet.Where(x => x.ParentId == parentId)
                .ToListAsync();
        }
    }
}

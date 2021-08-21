using System;
using System.Collections.Generic;
using System.Text;
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
    }
}

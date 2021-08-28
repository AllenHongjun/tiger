using System;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Stock
{
    public class ReverseDetailRepository : EfCoreRepository<TigerDbContext, ReverseDetail, Guid>, IReverseDetailRepository
    {
        public ReverseDetailRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
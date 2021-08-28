using System;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Business.Members
{
    public class MemberLevelRepository : EfCoreRepository<TigerDbContext, MemberLevel, Guid>, IMemberLevelRepository
    {
        public MemberLevelRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
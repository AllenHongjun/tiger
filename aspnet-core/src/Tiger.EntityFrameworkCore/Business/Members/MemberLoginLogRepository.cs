using System;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Business.Members
{
    public class MemberLoginLogRepository : EfCoreRepository<TigerDbContext, MemberLoginLog, Guid>, IMemberLoginLogRepository
    {
        public MemberLoginLogRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
using System;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Business.Members
{
    public class MemberStatisticInfoRepository : EfCoreRepository<TigerDbContext, MemberStatisticInfo, Guid>, IMemberStatisticInfoRepository
    {
        public MemberStatisticInfoRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
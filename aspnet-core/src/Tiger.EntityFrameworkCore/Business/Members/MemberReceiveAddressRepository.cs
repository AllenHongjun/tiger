using System;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Business.Members
{
    public class MemberReceiveAddressRepository : EfCoreRepository<TigerDbContext, MemberReceiveAddress, Guid>, IMemberReceiveAddressRepository
    {
        public MemberReceiveAddressRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
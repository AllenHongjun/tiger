using System;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Business.Members
{
    public interface IMemberReceiveAddressRepository : IRepository<MemberReceiveAddress, Guid>
    {
    }
}
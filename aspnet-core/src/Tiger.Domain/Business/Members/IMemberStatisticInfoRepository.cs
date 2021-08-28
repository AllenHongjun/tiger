using System;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Business.Members
{
    public interface IMemberStatisticInfoRepository : IRepository<MemberStatisticInfo, Guid>
    {
    }
}
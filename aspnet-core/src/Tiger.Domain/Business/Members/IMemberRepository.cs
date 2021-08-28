using System;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Business.Members
{
    public interface IMemberRepository : IRepository<Member, Guid>
    {
    }
}
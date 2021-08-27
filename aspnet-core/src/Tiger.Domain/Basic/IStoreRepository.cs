using System;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Basic
{
    public interface IStoreRepository : IRepository<Store, Guid>
    {
    }
}
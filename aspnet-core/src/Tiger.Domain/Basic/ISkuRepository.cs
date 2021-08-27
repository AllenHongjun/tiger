using System;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Basic
{
    public interface ISkuRepository : IRepository<Sku, Guid>
    {
    }
}
using System;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Basic
{
    public interface IWarehouseRepository : IRepository<Warehouse, Guid>
    {
    }
}
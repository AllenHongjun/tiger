using System;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Stock
{
    public interface IInventoryRepository : IRepository<Inventory, Guid>
    {
    }
}
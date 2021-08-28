using System;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Stock
{
    public interface IInventoryHistoryRepository : IRepository<InventoryHistory, Guid>
    {
    }
}
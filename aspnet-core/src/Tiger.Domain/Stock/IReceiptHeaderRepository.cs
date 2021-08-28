using System;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Stock
{
    public interface IReceiptHeaderRepository : IRepository<ReceiptHeader, Guid>
    {
    }
}
using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Stock
{
    public interface IReceiptHeaderRepository : IRepository<ReceiptHeader, Guid>
    {
        Task<ReceiptHeader> GetAsync(Guid id);
    }
}
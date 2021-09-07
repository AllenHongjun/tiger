using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Stock
{
    public interface ITransferHeaderRepository : IRepository<TransferHeader, Guid>
    {
        Task<TransferHeader> GetAsync(Guid id);
    }
}
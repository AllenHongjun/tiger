using System;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Stock
{
    public interface ITransferDetailRepository : IRepository<TransferDetail, Guid>
    {
    }
}
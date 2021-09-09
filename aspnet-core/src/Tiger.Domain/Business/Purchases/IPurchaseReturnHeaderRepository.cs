using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Business.Purchases
{
    public interface IPurchaseReturnHeaderRepository : IRepository<PurchaseReturnHeader, Guid>
    {
        Task<PurchaseReturnHeader> GetAsync(Guid id);
    }
}
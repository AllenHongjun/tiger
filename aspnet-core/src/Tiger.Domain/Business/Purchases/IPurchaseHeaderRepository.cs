using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Business.Purchases
{
    public interface IPurchaseHeaderRepository : IRepository<PurchaseHeader, Guid>
    {
        Task<PurchaseHeader> GetAsync(Guid id);
    }
}
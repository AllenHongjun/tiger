using System;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Business.Purchases
{
    public interface IPurchaseReturnDetailRepository : IRepository<PurchaseReturnDetail, Guid>
    {
    }
}
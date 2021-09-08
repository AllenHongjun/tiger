using System;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Business.Orders
{
    public interface IOrderReturnDetailRepository : IRepository<OrderReturnDetail, Guid>
    {
    }
}
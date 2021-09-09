using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Business.Orders
{
    public interface IOrderReturnHeaderRepository : IRepository<OrderReturnHeader, Guid>
    {
        Task<OrderReturnHeader> GetAsync(Guid id);
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Orders;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Business.Orders
{
    public interface IOrderItemRepository : IRepository<OrderItem, Guid>
    {
    }
}

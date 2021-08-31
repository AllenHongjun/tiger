using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Business.Orders
{
    public interface IOrderRepository : IRepository<Order, Guid>
    {

        /// <summary>
        /// 生成订单
        /// </summary>
        /// <param name="createOrderDto"></param>
        /// <returns></returns>
        Task<Order> CreateOrder(Guid memberId, int sourceType, int orderType, int useIntegration);
    }
}

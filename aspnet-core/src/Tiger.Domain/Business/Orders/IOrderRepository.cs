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
        /// 查询订单列表
        /// </summary>
        /// <param name="skipCount"></param>
        /// <param name="maxResultCount"></param>
        /// <param name="sorting"></param>
        /// <param name="filter"></param>
        /// <param name="status"></param>
        /// <param name="paytype"></param>
        /// <param name="sourceType"></param>
        /// <returns></returns>
        Task<List<Order>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter,
            int? status,
            int? paytype,
            int? sourceType
            );

        Task<int> TotalCount(
            string filter,
            int? status,
            int? paytype,
            int? sourceType
            );

        /// <summary>
        /// 生成订单
        /// </summary>
        /// <param name="createOrderDto"></param>
        /// <returns></returns>
        Task<Order> CreateOrder(Guid memberId, int sourceType, int orderType, int useIntegration);
    }
}

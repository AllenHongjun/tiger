using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Orders;
using Tiger.Orders.OrderOperateHistorys;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Order
{
    [RemoteService(false)]
    public class OrderOperationHistoryAppService :
        CrudAppService<
            OrderOperateHistory,
            OrderOperationHistoryDto,
            Guid,
            PagedAndSortedResultRequestDto, //Used for paging/sorting
            CreateUpdateOrderOperationHistoryDto> //Used to create/update
    {
        public OrderOperationHistoryAppService(IRepository<OrderOperateHistory, Guid> repository) : base(repository)
        {
        }
    }
}

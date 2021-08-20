using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Orders.OrderOperateHistorys
{
    public interface IOrderOperationHistoryAppservice:
        ICrudAppService<
            OrderOperationHistoryDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateOrderOperationHistoryDto>
    {

    }
}

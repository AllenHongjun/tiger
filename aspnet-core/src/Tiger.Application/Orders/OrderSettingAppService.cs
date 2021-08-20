using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Orders;
using Tiger.Orders.OrderSettings;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Order
{
    public class OrderSettingAppService :
        CrudAppService<
            OrderSetting,
            OrderSettingDto,
            Guid,
            PagedAndSortedResultRequestDto, //Used for paging/sorting
            CreateUpdateOrderSettingDto> //Used to create/update
    {
        public OrderSettingAppService(IRepository<OrderSetting, Guid> repository) : base(repository)
        {
        }
    }
}

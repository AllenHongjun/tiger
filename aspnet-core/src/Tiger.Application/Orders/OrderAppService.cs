using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Tiger.Orders.Orders;
using Tiger.Business;
using Tiger.Business.Orders;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Orders
{
    public class OrderAppService :
        CrudAppService<
            Business.Orders.Order, 
            OrderDto, 
            Guid,
            GetOrderListDto, //Used for paging/sorting
            CreateUpdateOrderDto,
            CreateUpdateOrderDto>, //Used to create/update
        IOrderAppService 
    {
        public OrderAppService(IRepository<Business.Orders.Order, Guid> repository) : base(repository)
        {
        }
    }
}

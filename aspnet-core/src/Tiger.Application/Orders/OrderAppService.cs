using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Tiger.Orders.Orders;
using Tiger.Business;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Orders
{
    public class OrderAppService :
        CrudAppService<
            Business.Order, 
            OrderDto, 
            Guid, 
            PagedAndSortedResultRequestDto, //Used for paging/sorting
            CreateUpdateOrderDto>, //Used to create/update
        IOrderAppService 
    {
        public OrderAppService(IRepository<Business.Order, Guid> repository) : base(repository)
        {
        }
    }
}

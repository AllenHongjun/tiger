using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Orders;
using Tiger.Orders.OrderItems;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Order
{
    public class OrderItemAppService :
        CrudAppService<
            OrderItem,
            OrderItemDto,
            Guid,
            PagedAndSortedResultRequestDto, //Used for paging/sorting
            CreateUpdateOrderItemDto>, //Used to create/update
        IOrderItemAppService
    {
        public OrderItemAppService(IRepository<OrderItem, Guid> repository) : base(repository)
        {
        }
    }
}

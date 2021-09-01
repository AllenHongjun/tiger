using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tiger.Business.Orders;
using Tiger.Business.Orders.Dtos;
using Tiger.Orders.Orders;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;

namespace Tiger.Orders
{
    [RemoteService(false)]
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
        protected readonly IOrderRepository _orderRepository;
        protected readonly IRepository<CartItem, Guid> _cartIteamRepository;
        protected readonly OrderManager _orderManager;
        public OrderAppService(
            IOrderRepository repository,
            IRepository<CartItem, Guid> cartIteamRepository,
            OrderManager orderManager

            ) : base(repository)
        {
            _orderRepository = repository;
            _cartIteamRepository = cartIteamRepository;
            _orderManager = orderManager;
        }

        /// <summary>
        /// 生成订单
        /// </summary>
        /// <param name="createOrderDto"></param>
        /// <returns></returns>
        public async Task<OrderDto> CreateOrder(CreateOrderDto createOrderDto)
        {
            List<CartItem> cartItems =  _cartIteamRepository.Where(x => x.MemberId == createOrderDto.memberId).ToList();
            if (cartItems == null)
            {
                throw new Exception("请先将商品加入购物车");
            }

            var order = await _orderManager.CreateOrder(createOrderDto.memberId, createOrderDto.sourceType, createOrderDto.orderType, createOrderDto.useIntegration);

            await CurrentUnitOfWork.SaveChangesAsync();

            //TODO: 生成订单成功 清空购物车

            //await _cartIteamRepository.DeleteAsync(x => cartItems.Any(c => c.Id == x.Id));

            var orderDto = ObjectMapper.Map<Business.Orders.Order, OrderDto>(order);
            return orderDto;
        }
    }
}

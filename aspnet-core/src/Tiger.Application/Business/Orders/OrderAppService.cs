using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tiger.Business.Orders;
using Tiger.Business.Orders.Dtos;
using Tiger.Orders.Orders;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.ObjectMapping;

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
        /// 分页查询书籍的数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<PagedResultDto<OrderDto>>
            GetListAsync(GetOrderListDto input)
        {
            await CheckGetListPolicyAsync();

            var query = await _orderRepository.GetListAsync(input.SkipCount, input.MaxResultCount, input.Sorting, input.Filter, input.Status, input.PayType, input.SourceType);

            var orderDtos = ObjectMapper.Map<List<Business.Orders.Order>, List<OrderDto>>(query);

            //Get the total count with another query
            var totalCount = await _orderRepository.TotalCount(input.Filter, input.Status, input.PayType, input.SourceType);

            return new PagedResultDto<OrderDto>(
                totalCount,
                orderDtos
            );
        }


        /// <summary>
        /// 生成订单
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<OrderDto> CreateOrder(CreateOrderDto input)
        {
            List<CartItem> cartItems =  _cartIteamRepository.Where(x => x.MemberId == input.memberId).ToList();
            if (cartItems == null)
            {
                throw new Exception("请先将商品加入购物车");
            }

            var order = await _orderRepository.CreateOrder(
                input.memberId, 
                input.sourceType, 
                input.orderType, 
                input.useIntegration);

            //await CurrentUnitOfWork.SaveChangesAsync();

            //TODO: 生成订单成功 清空购物车

            var orderDto = ObjectMapper.Map<Business.Orders.Order, OrderDto>(order);
            return orderDto;
        }
    }
}

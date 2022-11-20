using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger.Orders;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Tiger.Business.Orders
{
    public class OrderManager: DomainService
    {
        protected IOrderRepository _orderRepository;
        protected IOrderItemRepository _orderItemRepository;
        protected ICartItemRepository _cartItemsRepository;
        public OrderManager(
            IOrderRepository orderRepository, 
            IOrderItemRepository orderItemRepository,
            ICartItemRepository cartItemsRepository

            )
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _cartItemsRepository = cartItemsRepository;
        }


        #region 生成订单
        /// <summary>
        /// 生成订单
        /// </summary>
        /// <param name="createOrderDto"></param>
        /// <returns></returns>
        public virtual async Task<Order> CreateOrder(Guid memberId, int sourceType, int orderType, int useIntegration)
        {
            List<CartItem> cartItems = _cartItemsRepository.Where(x => x.MemberId == memberId).ToList();
            if (cartItems == null)
            {
                throw new Exception("请先将商品加入购物车");
            }



            Order order = new Order();

            order.Code = Guid.NewGuid().ToString();
            order.TotalAmount = cartItems.Sum(x => x.Price);
            order.PayAmount = cartItems.Sum(x => x.Price);
            order.FreightAmount = 0;
            order.PromotionAmount = 0;
            order.IntegrationAmount = 0;
            order.CouponAmount = 0;
            order.DiscountAmount = 0;
            order.PayType = 0;
            order.SourceType = sourceType;
            order.OrderType = orderType;
            order.ConfirmStatus = 0;
            order.UseIntegration = useIntegration;
            order.MemberId = memberId;
            //DbContext.Entry(order).State = EntityState.Detached;

            //List<OrderItem> orderItems = new List<OrderItem>();

            foreach (var cartItem in cartItems)
            {
                OrderItem orderItem = new OrderItem();
                //orderItem.Id = SequentialGuidGenerator.Instance.Create();
                orderItem.OrderCode = order.Code;
                orderItem.ProductPic = cartItem.ProductPic;
                orderItem.ProductName = cartItem.ProductName;
                orderItem.ProductSn = cartItem.ProductSn;
                orderItem.ProductPrice = cartItem.Price;
                orderItem.ProductQuantity = cartItem.Quantity;
                orderItem.ProductSkuCode = "";
                //orderItem.ProductCategoryId = cartItem.CategoryId;
                orderItem.PromotionName = "";
                orderItem.PromotionAmount = 0;
                orderItem.CouponAmount = 0;
                orderItem.IntegrationAmount = 0;
                orderItem.RealAmount = cartItem.Price;
                orderItem.GiftGrowth = 0;
                orderItem.ProductAttr = "";
                //orderItem.SkuId = ;
                //orderItem.OrderId = order.Id;
                orderItem.ProductId = cartItem.ProductId;
                //orderItems.Add(orderItem);
                order.OrderItems.Add(orderItem);
                //await _orderItemRepository.InsertAsync(orderItem);

            }

            await _orderRepository.InsertAsync(order);



            return order;
        }
        #endregion


        #region 仓储 Get/Find 方法 包含子对象测试
        /// <summary>
        /// 获取一个包含子对象的 order
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task TestWithDetails(Guid id)
        {
            var order = await _orderRepository.GetAsync(id);
        }


        /// <summary>
        /// 获取一个不包含子对象的 order
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task TestWithoutDetails(Guid id)
        {
            // 显式 / 延迟加载

            //order.OrderItems 此时是空的
            var order = await _orderRepository.GetAsync(id, includeDetails: false);

            //order.OrderItems 被填充
            await _orderRepository.EnsureCollectionLoadedAsync(order, x => x.OrderItems);
            
        }

        /// <summary>
        /// 获取一个包含子对象的实体列表
        /// </summary>
        /// <returns></returns>
        public async Task TestWithDetails()
        {
            var orders = await _orderRepository.GetListAsync(includeDetails: true);
        } 
        #endregion




    }
}

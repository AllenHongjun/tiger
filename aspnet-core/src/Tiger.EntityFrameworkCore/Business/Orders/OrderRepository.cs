/**
 * 类    名：BookRepository   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/16 14:13:25       
 * 说    明: 
 * 
 */

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger.Books;
using Tiger.Business.Demo;
using Tiger.Business.Orders;
using Tiger.Domain.CoreModule.Utilities;
using Tiger.EntityFrameworkCore;
using Tiger.Orders;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Demo
{   
    /// <summary>
    /// 书籍仓储层的实现
    /// </summary>
    public class OrderRepository : EfCoreRepository<TigerDbContext, Order, Guid>, IOrderRepository
    {
        public OrderRepository(IDbContextProvider<TigerDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }


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
        public async Task<List<Order>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter ,
            int? status,
            int? paytype,
            int? sourceType,
            DateTime? dateStart,
            DateTime? dateEnd
            )
        {
            return await DbSet
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    order => order.OrderSn.Contains(filter)
                    || order.ReceiverName.Contains(filter)
                 )
                
                .WhereIf(status.HasValue, order => order.Status == status)
                .WhereIf(paytype.HasValue, order => order.PayType == paytype)
                .WhereIf(sourceType.HasValue, order => order.SourceType == sourceType)
                .WhereIf(dateStart.HasValue, order => order.CreationTime >= dateStart)
                .WhereIf(dateEnd.HasValue, order => order.CreationTime <= dateEnd)
                .OrderByDescending(sorting => sorting.CreationTime)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }

        public async Task<int> TotalCount(
            string filter,
            int? status,
            int? paytype,
            int? sourceType
            )
        {
            return await DbSet
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    order => order.OrderSn.Contains(filter)
                    || order.ReceiverName.Contains(filter)
                 )

                .WhereIf(status.HasValue, order => order.Status == status)
                .WhereIf(paytype.HasValue, order => order.PayType == paytype)
                .WhereIf(sourceType.HasValue, order => order.SourceType == sourceType)
                .CountAsync();
        }



        /// <summary>
        /// 生成订单
        /// </summary>
        /// <param name="createOrderDto"></param>
        /// <returns></returns>
        public async Task<Order> CreateOrder(
            Guid memberId, 
            int sourceType, 
            int orderType, 
            int useIntegration
            )
        {
            List<CartItem> cartItems = DbContext.Set<CartItem>().Where(x => x.MemberId == memberId).ToList();
            if (cartItems == null)
            {
                throw new Exception("请先将商品加入购物车");
            }

            Order order = new Order();
            order.OrderSn = Utility.CreateOrderID("PT");
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
            
            foreach (var cartItem in cartItems)
            {
                OrderItem orderItem = new OrderItem();
                orderItem.OrderSn = order.OrderSn;
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

                order.OrderItems.Add(orderItem);

                //DbContext.Entry(orderItem).State = EntityState.Detached;
            }

            await DbSet.AddAsync(order);
            await DbContext.SaveChangesAsync();

            //TODO: 生成订单成功 清空购物车

            //await _cartIteamRepository.DeleteAsync(x => cartItems.Any(c => c.Id == x.Id));

            return order;
        }

        //public async Task DeleteBooksByType(BookType type)
        //{   
        //    // 直接指向sql 语句的代码
        //    await DbContext.Database.ExecuteSqlRawAsync(
        //        $"DELETE FROM Books WHERE Type = {(int)type}"
        //    );
        //}
    }
}

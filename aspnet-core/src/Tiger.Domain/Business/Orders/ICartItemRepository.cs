using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Orders;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Business.Orders
{
    public interface ICartItemRepository : IRepository<CartItem, Guid>
    {

        /// <summary>
        /// 商品添加购物车
        /// </summary>
        /// <param name="productId">商品id</param>
        /// <param name="SkuId">SkuIdid</param>
        /// <param name="MemberId">会员id</param>
        /// <returns></returns>
        Task<CartItem> AddToCartItem(Guid productId, Guid skuId, Guid memberId);
    }
}

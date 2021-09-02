using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger.Basic;
using Tiger.EntityFrameworkCore;
using Tiger.Orders;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Business.Orders
{
    public class CartItemRepository : EfCoreRepository<TigerDbContext, CartItem, Guid>, ICartItemRepository
    {
        public CartItemRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }


        /// <summary>
        /// 商品添加购物车
        /// </summary>
        /// <param name="productId">商品id</param>
        /// <param name="SkuId">SkuIdid</param>
        /// <param name="MemberId">会员id</param>
        /// <returns></returns>
        public async Task<CartItem> AddToCartItem(Guid productId, Guid skuId, Guid memberId)
        {
            Product product = await DbContext.Set<Product>().FindAsync(productId);
            CartItem cartItem = DbSet.Where(x => x.MemberId == memberId && x.ProductId == productId).FirstOrDefault();
            if (cartItem == null)
            {
                cartItem = new CartItem(product.Price, product.Name, "", product.ProductSn, "", null, productId, memberId, product.ProductCategoryId);
                cartItem.Quantity += 1;

                await DbSet.AddAsync(cartItem);
            }
            else
            {
                cartItem.Quantity += 1;
                DbSet.Update(cartItem);
            }

            return cartItem;

        }


        
    }
}

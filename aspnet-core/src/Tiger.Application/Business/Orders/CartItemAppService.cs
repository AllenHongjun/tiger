using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger.Basic;
using Tiger.Business.Orders;
using Tiger.Orders.CartItems;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;

namespace Tiger.Orders
{
    /// <summary>
    /// 购物车
    /// 
    /// ICartItemAppService 注意实现接口的中 泛型的类型必须一致
    /// </summary>
    [RemoteService(false)]
    public class CartItemAppService :
        CrudAppService<
            CartItem, //The  entity
            CartItemDto,
            Guid, //Primary key 
            GetCartItemListDto,
            CreateUpdateCartItemDto,
            CreateUpdateCartItemDto>, //Used to create/update
        ICartItemAppService
    {
        IRepository<Product, Guid> _productRepository;
        ICartItemRepository _cartItemRepository;

        public CartItemAppService(ICartItemRepository repository,
            IRepository<Product, Guid> productRepository
            ) : base(repository)
        {
            _productRepository = productRepository;
            _cartItemRepository = repository;
        }


        /// <summary>
        /// 商品添加购物车
        /// </summary>
        /// <param name="productId">商品id</param>
        /// <param name="SkuId">SkuIdid</param>
        /// <param name="MemberId">会员id</param>
        /// <returns></returns>
        public async Task<CartItemDto>  AddToCartItem(Guid productId, Guid skuId, Guid memberId)
        {

            CartItem cartItem = await _cartItemRepository.AddToCartItem(productId, skuId, memberId);

            return ObjectMapper.Map<CartItem, CartItemDto>(cartItem);

        }

    }
}

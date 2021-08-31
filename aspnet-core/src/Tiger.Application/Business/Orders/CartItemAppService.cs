using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger.Basic;
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
        IRepository<CartItem, Guid> _cartItemRepository;

        public CartItemAppService(IRepository<CartItem, Guid> repository,
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
        public async Task<CartItemDto>  AddToCartItem(Guid productId, Guid SkuId, Guid MemberId)
        {
            Product product = await _productRepository.GetAsync(productId);
            CartItem cartItem = _cartItemRepository.Where(x => x.MemberId == MemberId && x.ProductId == productId).FirstOrDefault();
            if (cartItem == null)
            {
                cartItem = new CartItem();
                cartItem.Quantity += 1;
                cartItem.Price = product.Price;
                cartItem.ProductPic = product.Picture;
                cartItem.ProductName = product.Name;
                cartItem.ProductSubTitle = product.SubTitle;
                cartItem.ProductSn = product.ProductSn;

                cartItem.ProductId = productId;
                cartItem.CategoryId = product.ProductCategoryId;

                //cartItem.SkuId = Guid.NewGuid();
                cartItem.MemberId = MemberId; 

                cartItem = await _cartItemRepository.InsertAsync(cartItem);
            }
            else
            {
                cartItem.Quantity += 1;
                cartItem = await _cartItemRepository.UpdateAsync(cartItem);
            }

            return ObjectMapper.Map<CartItem, CartItemDto>(cartItem);

        }

    }
}

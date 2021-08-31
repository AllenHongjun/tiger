using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Orders.CartItems
{
    public interface ICartItemAppService:
         ICrudAppService<
             CartItemDto, // Used to show category
            Guid,
            GetCartItemListDto,
            CreateUpdateCartItemDto,
            CreateUpdateCartItemDto>
    {
        /// <summary>
        /// 商品添加购物车
        /// </summary>
        /// <param name="productId">商品id</param>
        /// <param name="SkuId">Skuid</param>
        /// <param name="MemberId">会员id</param>
        /// <returns></returns>
        Task<CartItemDto> AddToCartItem(Guid productId, Guid SkuId, Guid MemberId);
    }
}

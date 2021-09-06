using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Orders.CartItems;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Tiger.Controllers.WebApp
{
    [RemoteService(Name = "CartItem")]
    [Area("WebApp")]
    [ControllerName("CartItem")]
    [Route("api/webapp/cartitem")]
    [ApiExplorerSettings(GroupName = "api")]
    public class CartItemController : TigerController
    {
        protected readonly ICartItemAppService _cartItemAppService;

        public CartItemController(ICartItemAppService cartItemAppService)
        {
            _cartItemAppService = cartItemAppService;
        }

        /// <summary>
        /// 商品添加购物车
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [Route("{productId}")]
        [HttpPost]
        public async Task<CartItemDto> AddToCartItem(Guid productId, Guid memberId)
        {
            return await  _cartItemAppService.AddToCartItem(productId, SkuId, memberId);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<CartItemDto> CreateAsync(CreateUpdateCartItemDto input)
        {
            return _cartItemAppService.CreateAsync(input);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public Task DeleteAsync(Guid id)
        {
            return _cartItemAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 获取单条
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public Task<CartItemDto> GetAsync(Guid id)
        {
            return _cartItemAppService.GetAsync(id);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        //[Route("all")]
        public Task<PagedResultDto<CartItemDto>> GetListAsync(GetCartItemListDto input)
        {
            return _cartItemAppService.GetListAsync(input);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public Task<CartItemDto> UpdateAsync(Guid id, CreateUpdateCartItemDto input)
        {
            return _cartItemAppService.UpdateAsync(id, input);
        }
    }
}

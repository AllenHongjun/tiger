using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Orders.CartItems;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

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
        public CartItemAppService(IRepository<CartItem, Guid> repository) : base(repository)
        {
        }
    }
}

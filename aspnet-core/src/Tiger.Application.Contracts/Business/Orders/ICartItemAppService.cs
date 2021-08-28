using System;
using System.Collections.Generic;
using System.Text;
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

    }
}

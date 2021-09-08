using System;
using Tiger.Business.Purchases.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Business.Purchases
{
    public interface IPurchaseHeaderAppService :
        ICrudAppService< 
            PurchaseHeaderDto, 
            Guid, 
            PagedAndSortedResultRequestDto,
            CreateUpdatePurchaseHeaderDto,
            CreateUpdatePurchaseHeaderDto>
    {

    }
}
using System;
using Tiger.Business.Purchases.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Business.Purchases
{
    public interface IPurchaseReturnDetailAppService :
        ICrudAppService< 
            PurchaseReturnDetailDto, 
            Guid, 
            PagedAndSortedResultRequestDto,
            CreateUpdatePurchaseReturnDetailDto,
            CreateUpdatePurchaseReturnDetailDto>
    {

    }
}
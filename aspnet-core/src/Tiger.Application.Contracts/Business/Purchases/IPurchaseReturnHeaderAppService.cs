using System;
using Tiger.Business.Purchases.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Business.Purchases
{
    public interface IPurchaseReturnHeaderAppService :
        ICrudAppService< 
            PurchaseReturnHeaderDto, 
            Guid, 
            PagedAndSortedResultRequestDto,
            CreateUpdatePurchaseReturnHeaderDto,
            CreateUpdatePurchaseReturnHeaderDto>
    {

    }
}
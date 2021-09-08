using System;
using Tiger.Business.Purchases.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Business.Purchases
{
    public interface IPurchaseDetailAppService :
        ICrudAppService< 
            PurchaseDetailDto, 
            Guid, 
            PagedAndSortedResultRequestDto,
            CreateUpdatePurchaseDetailDto,
            CreateUpdatePurchaseDetailDto>
    {

    }
}
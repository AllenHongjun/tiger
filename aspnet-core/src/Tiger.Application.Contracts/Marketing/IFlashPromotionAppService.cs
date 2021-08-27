using System;
using Tiger.Marketing.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Marketing
{
    public interface IFlashPromotionAppService :
        ICrudAppService< 
            FlashPromotionDto, 
            Guid, 
            PagedAndSortedResultRequestDto,
            CreateUpdateFlashPromotionDto,
            CreateUpdateFlashPromotionDto>
    {

    }
}
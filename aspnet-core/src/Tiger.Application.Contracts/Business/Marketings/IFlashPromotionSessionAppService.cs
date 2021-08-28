using System;
using Tiger.Marketing.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Marketing
{
    public interface IFlashPromotionSessionAppService :
        ICrudAppService< 
            FlashPromotionSessionDto, 
            Guid, 
            PagedAndSortedResultRequestDto,
            CreateUpdateFlashPromotionSessionDto,
            CreateUpdateFlashPromotionSessionDto>
    {

    }
}
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Services;

namespace Tiger.Marketings.CouponHistorys
{
    public interface ICouponHistoryAppService:
         ICrudAppService<
            CouponHistoryDto, // Used to show category
            Guid,
            GetCouponHistoryListDto,
            CreateUpdateCouponHistoryDto,
            CreateUpdateCouponHistoryDto>
    {
    }
}

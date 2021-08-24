using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Marketing;
using Tiger.Marketings.CouponHistorys;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Marketings
{   
    /// <summary>
    /// 优惠券领取记录
    /// </summary>
    public class CouponHistoryAppService :
        CrudAppService<
            CouponHistory, //The  entity
            CouponHistoryDto,
            Guid, //Primary key 
            GetCouponHistoryListDto,  //Used for paging/sorting
            CreateUpdateCouponHistoryDto,
            CreateUpdateCouponHistoryDto>, //Used to create/update
        ICouponHistoryAppService

    {
        public CouponHistoryAppService(IRepository<CouponHistory, Guid> repository) : base(repository)
        {
        }
    }
}

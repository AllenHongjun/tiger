using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Tiger.Marketings.CouponHistorys
{
    public class GetCouponHistoryListDto : PagedAndSortedResultRequestDto
    {
        /// <summary>
        /// 查询关键字
        /// </summary>
        public string Filter { get; set; }
        public int UseStatus { get; set; }
    }
}

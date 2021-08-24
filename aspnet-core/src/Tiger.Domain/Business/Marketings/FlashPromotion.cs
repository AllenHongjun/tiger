/**
 * 类    名：FlashPromotion   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/11 13:33:47       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tiger.Marketing
{
    /// <summary>
    /// 限制购物
    /// </summary>
    public class FlashPromotion: AuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 活动名称
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 上下线状态
        /// </summary>
        public int Status { get; set; }


        public virtual ICollection<FlashPromotionSession> FlashPromotionSessions { get; set; }
    }
}

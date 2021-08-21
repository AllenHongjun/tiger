/**
 * 类    名：FlashPromotionSession   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/11 13:42:17       
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
    /// 显示购物场次表
    /// </summary>
    public class FlashPromotionSession:AuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        /// <summary>
        /// 启用状态 0->不启用  1->启用
        /// </summary>
        public int Status { get; set; }

        public Guid FlashPromotionId { get; set; }

        public virtual FlashPromotion FlashPromotion { get; set; }

        
    }
}

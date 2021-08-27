/**
 * 类    名：FlashPromotionSession   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/11 13:42:17       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tiger.Business.Marketings;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tiger.Marketing
{
    /// <summary>
    /// 显示购物场次表
    /// </summary>
    public class FlashPromotionSession:AuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 时间段名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 每日开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 每日结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 启用状态 0->不启用  1->启用
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 限制购物活动id
        /// </summary>
        public Guid FlashPromotionId { get; set; }

        [ForeignKey("FlashPromotionId")]
        public virtual FlashPromotion FlashPromotion { get; set; }

        public virtual ICollection<FlashPromotionProductRelation> FlashPromotionProductRelations { get; set; }

        
    }
}

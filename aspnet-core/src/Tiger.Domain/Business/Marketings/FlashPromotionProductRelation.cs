using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tiger.Basic;
using Tiger.Marketing;
using Volo.Abp.Domain.Entities;

namespace Tiger.Business.Marketings
{   
    /// <summary>
    /// 限时购物与商品关系表
    /// </summary>
    public class FlashPromotionProductRelation:Entity<Guid>
    {
        /// <summary>
        /// 限时购数量
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 每人限购数量
        /// </summary>
        public int Count { get; set; }

        public int Sort { get; set; }

        public Guid FlashPromotionId { get; set; }

        [ForeignKey("FlashPromotionId")]
        public virtual FlashPromotion FlashPromotion { get; set; }

        public Guid FlashPromotionSessionId { get; set; }

        [ForeignKey("FlashPromotionSessionId")]
        public virtual FlashPromotionSession FlashPromotionSession { get; set; }

        public Guid PrductId { get; set; }

        [ForeignKey("PrductId")]
        public virtual Product Product { get; set; }
    }
}

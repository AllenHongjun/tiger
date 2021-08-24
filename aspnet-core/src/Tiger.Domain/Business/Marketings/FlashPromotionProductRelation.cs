using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace Tiger.Business.Marketings
{   
    /// <summary>
    /// 限时购物与商品关系表
    /// </summary>
    public class FlashPromotionProductRelation:Entity<Guid>
    {
        public Guid FlashPromotionId { get; set; }

        public Guid FlashPromotionSessionId { get; set; }

        public Guid PrductId { get; set; }

        /// <summary>
        /// 限时购数量
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 每人限购数量
        /// </summary>
        public int Count { get; set; }

        public int Sort { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tiger.Business.Members
{   
    /// <summary>
    /// 用户信息统计表
    /// </summary>
    public class MemberStatisticInfo : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 累计消费金额
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// 累计下单次数
        /// </summary>
        public int OrderCount { get; set; }

        /// <summary>
        /// 优惠券用券
        /// </summary>
        public int CouponCount { get; set; }

        /// <summary>
        /// 评价数量
        /// </summary>
        public int CommentCount { get; set; }

        /// <summary>
        /// 退货数量
        /// </summary>
        public int ReturnOrderCount { get; set; }

        /// <summary>
        /// 登录次数
        /// </summary>
        public int LoginCount { get; set; }

        /// <summary>
        /// 关注数量
        /// </summary>
        public int AttentCount { get; set; }

        public int FanCount { get; set; }

        public int CollectProductCount { get; set; }

        public int CollectSubjectCount { get; set; }

        public int CollectTopicCount { get; set; }

        public int CollectCommentCount { get; set; }

        public int InviteFriendCount { get; set; }

        public DateTime RecentOrderTime { get; set; }
    }
}

using System;
using Volo.Abp.Application.Dtos;

namespace Tiger.Business.Members.Dtos
{
    [Serializable]
    public class MemberStatisticInfoDto : FullAuditedEntityDto<Guid>
    {
        public decimal TotalAmount { get; set; }

        public int OrderCount { get; set; }

        public int CouponCount { get; set; }

        public int CommentCount { get; set; }

        public int ReturnOrderCount { get; set; }

        public int LoginCount { get; set; }

        public int AttentCount { get; set; }

        public int FanCount { get; set; }

        public int CollectProductCount { get; set; }

        public int CollectSubjectCount { get; set; }

        public int CollectTopicCount { get; set; }

        public int CollectCommentCount { get; set; }

        public int InviteFriendCount { get; set; }

        public DateTime RecentOrderTime { get; set; }

        public Guid MemberId { get; set; }


    }
}
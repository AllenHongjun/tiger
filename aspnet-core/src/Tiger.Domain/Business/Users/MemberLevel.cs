using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace Tiger.Business.Users
{
    public class MemberLevel:Entity<Guid>
    {
        public string Name { get; set; }

        public int GrowthPoint { get; set; }

        /// <summary>
        /// 免运费标准
        /// </summary>
        public decimal FreeFreightPoint { get; set; }

        /// <summary>
        /// 每次评价获取的成长值
        /// </summary>
        public int CommentGrowthPoint { get; set; }

        /// <summary>
        /// 是否有免邮特权
        /// </summary>
        public int PriviledgeFreeFreight { get; set; }

        /// <summary>
        /// 是否有签到特权
        /// </summary>
        public int PriviledgeSignIn { get; set; }

        /// <summary>
        /// 是否有评论特权
        /// </summary>
        public int PrivilegeComment { get; set; }

        /// <summary>
        /// 是否有专享活动特权
        /// </summary>
        public int PrivilegePromotin { get; set; }

        /// <summary>
        /// 是否有会员价特权
        /// </summary>
        public int PrivilegeMemberPrice { get; set; }

        /// <summary>
        /// 是否有生日特权
        /// </summary>
        public int PrivilegeBirthDay { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public int Note { get; set; }
    }
}

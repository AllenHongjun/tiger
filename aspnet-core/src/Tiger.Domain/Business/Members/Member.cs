using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Tiger.Business.Members
{
    public class Member:FullAuditedAggregateRoot<Guid>, IMultiTenant, ISoftDelete
    {
       

        public string UserName { get; set; }

        public string Password { get; set; }

        public string NickName { get; set; }

        public string Phone { get; set; }

        /// <summary>
        /// 帐号启用状态:0->禁用；1->启用
        /// </summary>
        public int Status { get; set; }

        public string Icon { get; set; }

        /// <summary>
        /// 性别：0->未知；1->男；2->女
        /// </summary>
        public int Gender { get; set; }

        public DateTime BirthDay { get; set; }

        /// <summary>
        /// 所在城市
        /// </summary>
        public string City { get; set; }

        public string Job { get; set; }

        /// <summary>
        /// 用户来源
        /// </summary>
        public int SourceType { get; set; }

        /// <summary>
        /// 积分值
        /// </summary>
        public int Integration { get; set; }

        /// <summary>
        /// 成长值
        /// </summary>
        public int Growth { get; set; }

        /// <summary>
        /// 剩余抽奖次数
        /// </summary>
        public int LuckeyCount { get; set; }

        /// <summary>
        /// 历史积分数量
        /// </summary>
        public int HistoryIntegration { get; set; }

        public Guid MemberLevelId { get; set; }
        
        [ForeignKey("MemberLevelId")]
        public virtual MemberLevel MemberLevel { get; set; }

        public virtual ICollection<MemberLoginLog> MemberLoginLog { get; set; }

        public virtual ICollection<MemberReceiveAddress> MemberReceiveAddresses { get; set; }


        public Guid? TenantId { get; set; }
    }
}

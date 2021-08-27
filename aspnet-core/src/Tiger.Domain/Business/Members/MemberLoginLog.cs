using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tiger.Business.Members
{
    /// <summary>
    /// 会员登录记录表
    /// </summary>
    public class MemberLoginLog : FullAuditedAggregateRoot<Guid>
    {
        public Guid MemberId { get; set; }

        public virtual Member  Member { get; set; }
        /// <summary>
        /// 登录ip
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// 登录城市
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 登录类型：0->PC；1->android;2->ios;3->小程序
        /// </summary>
        public int LoginType { get; set; }

        /// <summary>
        /// 登录省份
        /// </summary>
        public string Province { get; set; }
    }
}

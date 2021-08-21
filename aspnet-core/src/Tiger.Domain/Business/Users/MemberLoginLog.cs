using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tiger.Business.Users
{
    public class MemberLoginLog : FullAuditedAggregateRoot<Guid>
    {
        public Guid MemberId { get; set; }

        public virtual Member  Member { get; set; }

        public string IP { get; set; }

        public string City { get; set; }

        /// <summary>
        /// 登录类型：0->PC；1->android;2->ios;3->小程序
        /// </summary>
        public int LoginType { get; set; }

        public string Province { get; set; }
    }
}

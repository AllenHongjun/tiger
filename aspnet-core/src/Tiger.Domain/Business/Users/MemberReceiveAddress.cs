using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace Tiger.Business.Users
{
    /// <summary>
    /// 用户配送地址
    /// </summary>
    public class MemberReceiveAddress:FullAuditedAggregateRoot<Guid>
    {
        public Guid MemberId { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public int DefaultStatus { get; set; }

        public string PostCode { get; set; }

        public string Province { get; set; }

        public string City { get; set; }

        /// <summary>
        /// 区
        /// </summary>
        public string Reginon { get; set; }

        /// <summary>
        /// 详细地址街道
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public string Lat { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public string Lon { get; set; }
    }
}

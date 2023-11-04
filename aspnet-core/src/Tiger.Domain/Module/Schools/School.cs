using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Tiger.Module.Schools
{
    /// <summary>
    /// 学校管理
    /// </summary>
    public class School:FullAuditedEntity<Guid>,IMultiTenant
    {
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 学校简称
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        /// 顺序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; }
        
        /// <summary>
        /// 编号
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// 授权截止时间
        /// </summary>
        public DateTime? ImpowerDate { get; set; }

        /// <summary>
        /// 最大人数
        /// </summary>
        public int MaxPerson { get; set; }

        /// <summary>
        /// 是否需要审核帖子
        /// </summary>
        public bool IsAudit { get; set; }

        /// <summary>
        /// Vip等级：1.免费客户 2.付费客户
        /// </summary>
        public VipLevel VipLevel { get; set; }
        

        protected School()
        {
        }

        public School(
            Guid id,
            Guid? tenantId,
            string name,
            string shortName,
            int sort,
            bool isEnable,
            string number,
            DateTime? impowerDate,
            int maxPerson,
            bool isAudit,
            VipLevel vipLevel
        ) : base(id)
        {
            TenantId = tenantId;
            Name = name;
            ShortName = shortName;
            Sort = sort;
            IsEnable = isEnable;
            Number = number;
            ImpowerDate = impowerDate;
            MaxPerson = maxPerson;
            IsAudit = isAudit;
            VipLevel = vipLevel;
        }
    }
}

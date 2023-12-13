using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Tiger.Module.QuestionBank
{
    /// <summary>
    /// 实训平台
    /// </summary>
    public class TrainPlatform : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// logo图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 实训跳转链接
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// token校验码
        /// </summary>
        public string CheckCode { get; set; }

        /// <summary>
        /// Token传值方式：0、使用旧版Cookie；1、使用旧版Url Token；2、使用新版Url Token；
        /// </summary>
        public TokenType TokenType { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int Sorting { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enable { get; set; }

        //public virtual ICollection<Question> Questions { get; set; }

    protected TrainPlatform()
    {
    }

    public TrainPlatform(
        Guid id,
        Guid? tenantId,
        string name,
        string description,
        string icon,
        string url,
        string checkCode,
        TokenType tokenType,
        int sorting,
        bool enable
    ) : base(id)
    {
        TenantId = tenantId;
        Name = name;
        Description = description;
        Icon = icon;
        Url = url;
        CheckCode = checkCode;
        TokenType = tokenType;
        Sorting = sorting;
        Enable = enable;
    }
    }
}

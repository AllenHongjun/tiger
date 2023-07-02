using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Tiger.Volo.Abp.Identity.Post
{
    /// <summary>
    /// 职位
    /// </summary>
    public class Post : FullAuditedAggregateRoot<Guid>, IMultiTenant, ISoftDelete
    {

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 职位编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 职位名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 状态启用/禁用
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// 租户Id
        /// </summary>
        public Guid? TenantId { get; set; } = Guid.Empty;


        protected Post()
        {
            /* 此构造函数用于反序列化/ORM使用 */
        }

        public Post(
            Guid id,
            string remark,
            string code,
            string name,
            int sort,
            bool isActive,
            Guid? tenantId
        ) : base(id)
        {
            Remark = remark;
            Code = code;
            Name = name;
            Sort = sort;
            IsActive = isActive;
            TenantId = tenantId;
        }


    }

}

using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Tiger.Module.TaskManagement
{
    /// <summary>
    /// 任务执行参数
    /// </summary>
    public class BackgroundJobAction:AuditedAggregateRoot<Guid>,IMultiTenant
    {
        public BackgroundJobAction()
        {
        }

        public BackgroundJobAction(
            Guid id,
            string jobId, 
            string name, 
            IDictionary<string, object> paramters, 
            Guid? tenantId):base(id)
        {
            JobId=Check.NotNullOrWhiteSpace(jobId,nameof(jobId),BackgroundJobActionConsts.MaxJobIdLength);
            Name=Check.NotNullOrWhiteSpace(name, nameof(name),BackgroundJobActionConsts.MaxNameLength);
            TenantId = tenantId;

            IsEnabled = true;
            Paramters = new Dictionary<string, object>();
            Paramters.AddIfNotContains(paramters);
        }

        /// <summary>
        /// 租户标识
        /// </summary>
        public virtual Guid? TenantId { get; protected set; }

        /// <summary>
        /// 任务标识
        /// </summary>
        public virtual string JobId { get; protected set; } 

        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name { get;protected set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public virtual bool IsEnabled { get; set; }

        /// <summary>
        /// 执行参数
        /// </summary>
        public virtual IDictionary<string, object> Paramters { get; set; }

    }
}

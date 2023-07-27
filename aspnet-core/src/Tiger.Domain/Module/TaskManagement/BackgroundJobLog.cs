using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace Tiger.Module.TaskManagement
{
    /// <summary>
    /// 任务日志
    /// </summary>
    public class BackgroundJobLog : Entity<long>, IMultiTenant
    {
        /// <summary>
        /// 租户标识
        /// </summary>
        public virtual Guid? TenantId { get; set; }
        /// <summary>
        /// 任务id
        /// </summary>
        public virtual string JobId { get; set; }
        /// <summary>
        /// 任务名称
        /// </summary>
        public virtual string JobName { get; set; }
        /// <summary>
        /// 任务分组
        /// </summary>
        public virtual string JobGroup { get; set; }

        /// <summary>
        /// 任务类型
        /// </summary>
        public virtual string JobType { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public virtual string Message { get; set; }

        /// <summary>
        /// 执行时间
        /// </summary>
        public virtual DateTime RunTime { get; set; }

        /// <summary>
        /// 异常信息
        /// </summary>
        public virtual string Exception { get; set; }

        public BackgroundJobLog()
        {
        }

        public BackgroundJobLog(
        string type,
        string group,
        string name,
        DateTime runTime,
        Guid? tenantId = null)
        {
            JobType = Check.NotNullOrWhiteSpace(type, nameof(type), BackgroundJobInfoConsts.MaxTypeLength);
            JobGroup = Check.NotNullOrWhiteSpace(group, nameof(group), BackgroundJobInfoConsts.MaxGroupLength);
            JobName = Check.NotNullOrWhiteSpace(name, nameof(name), BackgroundJobInfoConsts.MaxNameLength);
            RunTime = runTime;
            TenantId = tenantId;
        }

        public BackgroundJobLog SetMessage(string message, Exception ex)
        {
            Message = message.Length > BackgroundJobLogConsts.MaxMessageLength
            ? message.Substring(0, BackgroundJobLogConsts.MaxMessageLength - 1)
            : message;

            if (ex != null)
            {
                var errMsg = ex.ToString();
                Exception = errMsg.Length > BackgroundJobLogConsts.MaxExceptionLength
                ? errMsg.Substring(0, BackgroundJobLogConsts.MaxExceptionLength - 1)
                : errMsg;
            }
            return this;
        }
    }
}

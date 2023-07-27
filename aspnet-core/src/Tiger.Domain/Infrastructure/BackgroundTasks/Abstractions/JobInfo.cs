using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Module.TaskManagement.Enum;

namespace Tiger.Infrastructure.BackgroundTasks.Abstractions
{
    public class JobInfo
    {
        /// <summary>
        /// 任务标识
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 租户标识
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 任务分组
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// 任务类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 返回参数
        /// </summary>
        public string Result { get; set; }

        public JobSource Source { get; set; } = JobSource.None;

        public IDictionary<string, object> Args { get; set; } = new Dictionary<string, object>();

        public JobStatus Status { get; set; } = JobStatus.None;

        public string Description { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime BeginTime { get; set; }

        public DateTime? EndTime { get; set; }

        public DateTime? LastRunTime { get; set; }  

        public DateTime? NextRunTime { get; set; }

        public JobType JobType { get; set; } = JobType.Once;

        public string Cron { get; set; }

        
        public int TriggerCount { get; set; }

        /// <summary>
        /// 失败重试次数 默认50
        /// </summary>

        public int MaxTryCount { get; set; } = 50;

        /// <summary>
        /// 
        /// </summary>
        public int MaxCount { get; set; }
    }
}

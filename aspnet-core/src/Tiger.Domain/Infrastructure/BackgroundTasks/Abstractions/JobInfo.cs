﻿using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Infrastructure.BackgroundTasks.Abstractions.Enum;

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

        /// <summary>
        /// 作业来源
        /// </summary>
        public JobSource Source { get; set; } = JobSource.None;

        /// <summary>
        /// 任务参数
        /// </summary>
        public IDictionary<string, object> Args { get; set; }

        /// <summary>
        /// 任务状态
        /// </summary>
        public JobStatus Status { get; set; } = JobStatus.None;

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime BeginTime { get; set; }

        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 上次运行时间
        /// </summary>
        public DateTime? LastRunTime { get; set; }

        /// <summary>
        /// 下一次执行时间
        /// </summary>
        public DateTime? NextRunTime { get; set; }

        /// <summary>
        /// 任务类别
        /// </summary>
        public JobType JobType { get; set; } = JobType.Once;

        /// <summary>
        /// Cron表达式，如果是周期性任务需要指定
        /// </summary>
        public string Cron { get; set; }

        /// <summary>
        /// 触发次数
        /// </summary>
        public int TriggerCount { get; set; }

        /// <summary>
        /// 失败重试次数
        /// </summary>
        public int TryCount { get; set; }

        /// <summary>
        /// 失败重试上限 默认50
        /// </summary>
        public int MaxTryCount { get; set; } = 50;

        /// <summary>
        /// 最大执行次数
        /// 默认：0, 无限制
        /// </summary>
        public int MaxCount { get; set; }

        /// <summary>
        /// 连续失败且不会再次执行
        /// </summary>
        public bool IsAbandoned { get; set; }
        /// <summary>
        /// 间隔时间，单位秒，与Cron表达式冲突
        /// 默认: 300
        /// </summary>
        public int Interval { get; set; } = 300;
        /// <summary>
        /// 任务优先级
        /// </summary>
        public JobPriority Priority { get; set; } = JobPriority.Normal;
        /// <summary>
        /// 任务独占超时时长（秒）
        /// 0或更小不生效
        /// </summary>
        public int LockTimeOut { get; set; }
        /// <summary>
        /// 指定运行节点
        /// </summary>
        public string NodeName { get; set; }


        /// <summary>
        /// 计算作业可触发次数
        /// </summary>
        /// <returns></returns>
        public int CalcCanBeTriggered()
        {
            // 无限次
            var maxCount = -1;

            // 剩余次数
            if (MaxCount > 0)
            {
                maxCount = MaxCount - TriggerCount;
                if (maxCount < 0)
                {
                    maxCount = 0;
                }
            }

            // 一次
            if (JobType == JobType.Once)
            {
                maxCount = 1;
            }

            // 重试
            if (Status == JobStatus.FailedRetry && MaxTryCount > 0)
            {
                maxCount = MaxTryCount - TryCount;

                if (maxCount < 0)
                {
                    maxCount = 0;
                }

                if (maxCount > 0)
                {
                    // 触发重试时，失败间隔时间调整
                    Interval = 60;
                }
            }

            return maxCount;
        }
    }
}

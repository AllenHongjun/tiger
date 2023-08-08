﻿using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Collections;

namespace Tiger.Infrastructure.BackgroundTasks.Abstractions
{
    public class AbpBackgroundTasksOptions
    {
        /// <summary>
        /// 任务监听类型列表
        /// </summary>
        /// <remarks>
        /// 用户可以实现事件监听实现自定义逻辑
        /// </remarks>
        public ITypeList<IJobEvent> JobMonitors { get; }
        /// <summary>
        /// 作业提供者列表
        /// </summary>
        /// <remarks>
        /// 用户实现的作业可以添加在集合中
        /// </remarks>
        public ITypeList<IJobDefinitionProvider> DefinitionProviders { get; }
        /// <summary>
        /// 启用清理任务
        /// </summary>
        /// <remarks>
        /// 主节点启用
        /// </remarks>
        public bool JobCleanEnabled { get; set; }
        /// <summary>
        /// 任务过期时间
        /// 默认: 15 days
        /// </summary>
        /// <remarks>
        /// 任务过期时间,超出时间段清理
        /// </remarks>
        public TimeSpan JobExpiratime { get; set; }
        /// <summary>
        /// 每次清理任务批次大小
        /// 默认: 1000
        /// </summary>
        public int MaxJobCleanCount { get; set; }
        /// <summary>
        /// 清理过期任务批次Cron表达式
        /// 默认: 600秒（0 0/10 * * * ? * ）
        /// </summary>
        /// <remarks>
        /// Cron表达式
        /// </remarks>
        public string JobCleanCronExpression { get; set; }
        /// <summary>
        /// 启用轮询任务
        /// </summary>
        /// <remarks>
        /// 主节点启用
        /// </remarks>
        public bool JobFetchEnabled { get; set; }
        /// <summary>
        /// 每次轮询任务批次大小
        /// 默认: 1000
        /// </summary>
        public int MaxJobFetchCount { get; set; }
        /// <summary>
        /// 轮询任务批次Cron表达式
        /// 默认: 30秒（0/30 * * * * ? ）
        /// </summary>
        /// <remarks>
        /// Cron表达式
        /// </remarks>
        public string JobFetchCronExpression { get; set; }
        /// <summary>
        /// 轮询任务批次时锁定任务超时时长（秒）
        /// 默认：120
        /// </summary>
        /// <remarks>
        /// 轮询任务也属于一个后台任务, 需要对每一次轮询加锁，防止重复任务入库
        /// </remarks>
        public int JobFetchLockTimeOut { get; set; }
        /// <summary>
        /// 指定运行节点
        /// </summary>
        public string NodeName { get; set; }
        public AbpBackgroundTasksOptions()
        {
            JobFetchEnabled = false;
            MaxJobFetchCount = 1000;
            JobFetchLockTimeOut = 120;
            JobFetchCronExpression = "0/30 * * * * ? ";

            JobCleanEnabled = false;
            MaxJobCleanCount = 1000;
            JobExpiratime = TimeSpan.FromDays(15d);
            JobCleanCronExpression = "0 0/10 * * * ? *";

            JobMonitors = new TypeList<IJobEvent>();
            DefinitionProviders = new TypeList<IJobDefinitionProvider>();
        }
    }
}

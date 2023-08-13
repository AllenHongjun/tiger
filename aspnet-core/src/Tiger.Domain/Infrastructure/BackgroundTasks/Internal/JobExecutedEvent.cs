﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;
using Tiger.Infrastructure.BackgroundTasks.Abstractions;
using Tiger.Infrastructure.BackgroundTasks.Abstractions.Enum;
using Volo.Abp.DependencyInjection;

namespace Tiger.Infrastructure.BackgroundTasks.Internal;

/// <summary>
/// 作业执行事件处理
/// </summary>
public class JobExecutedEvent : JobEventBase<JobExecutedEvent>, ITransientDependency
{
    protected override async Task OnJobAfterExecutedAsync(JobEventContext context)
    {
        var store = context.ServiceProvider.GetRequiredService<IJobStore>();
        var job = await store.FindAsync(context.EventData.Key, context.EventData.CancellationToken);
        if (job != null)
        {
            job.TriggerCount += 1;
            job.TenantId = context.EventData.TenantId;
            job.LastRunTime = context.EventData.RunTime;
            job.NextRunTime = context.EventData.NextRunTime;
            job.Result = context.EventData.Result ?? "OK";
            job.Status = JobStatus.Running;

            // 一次性任务执行一次后标记为已完成
            if (job.JobType == JobType.Once)
            {
                job.Status = JobStatus.Completed;
            }

            // 任务异常后可重试
            if (context.EventData.Exception != null)
            {
                job.TryCount += 1;
                job.IsAbandoned = false;
                job.Result = GetExceptionMessage(context.EventData.Exception);

                // 周期性任务不用改变重试策略
                if (job.JobType != JobType.Period)
                {
                    // 将任务标记为运行中, 会被轮询重新进入队列
                    job.Status = JobStatus.FailedRetry;
                    // 多次异常后需要重新计算优先级
                    if (job.TryCount <= job.MaxTryCount / 2 &&
                        job.TryCount > job.MaxTryCount / 3)
                    {
                        job.Priority = JobPriority.BelowNormal;
                    }
                    else if (job.TryCount > job.MaxTryCount / 1.5)
                    {
                        job.Priority = JobPriority.Low;
                    }

                    // 等待时间调整
                    if (job.Interval <= 0)
                    {
                        job.Interval = 50;
                    }

                    var retryInterval = job.Interval * 1.5;
                    job.Interval = Convert.ToInt32(retryInterval);
                }

                // 当未设置最大重试次数时不会标记停止
                if (job.MaxTryCount > 0 && job.TryCount >= job.MaxTryCount)
                {
                    job.Status = JobStatus.Stopped;
                    job.IsAbandoned = true;
                    job.NextRunTime = null;
                    await RemoveJobAsync(context, job, context.EventData.CancellationToken);
                }
            }
            else
            {
                // 成功一次重置重试次数
                job.TryCount = 0;

                // 所有任务达到上限则标记已完成
                if (job.MaxCount > 0 && job.TriggerCount >= job.MaxCount)
                {
                    job.Status = JobStatus.Completed;
                    job.NextRunTime = null;

                    await RemoveJobAsync(context, job, context.EventData.CancellationToken);
                }
            }

            await store.StoreAsync(job, context.EventData.CancellationToken);
        }
    }

    private async Task RemoveJobAsync(JobEventContext context, JobInfo jobInfo, CancellationToken cancellationToken = default)
    {
        var jobScheduler = context.ServiceProvider.GetRequiredService<IJobScheduler>();
        await jobScheduler.RemoveAsync(jobInfo, cancellationToken);
    }

    private string GetExceptionMessage(Exception exception)
    {
        if (exception.InnerException != null)
        {
            return GetExceptionMessage(exception.InnerException);
        }

        return exception.Message;
    }
}

﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Tiger.Infrastructure.BackgroundTasks.Abstractions.Enum;
using Tiger.Infrastructure.BackgroundTasks.Abstractions;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;
using Volo.Abp.ObjectMapping;
using Tiger.Infrastructure.BackgroundTasks;

namespace Tiger.Module.TaskManagement
{
    public class BackgroundJobStore : IJobStore, ITransientDependency
    {
        protected IObjectMapper ObjectMapper { get; }
        protected ICurrentTenant CurrentTenant { get; }
        protected IUnitOfWorkManager UnitOfWorkManager { get; }
        protected IBackgroundJobInfoRepository JobInfoRepository { get; }
        protected IBackgroundJobLogRepository JobLogRepository { get; }

        public BackgroundJobStore(
            IObjectMapper objectMapper,
            ICurrentTenant currentTenant,
            IUnitOfWorkManager unitOfWorkManager,
            IBackgroundJobInfoRepository jobInfoRepository,
            IBackgroundJobLogRepository jobLogRepository)
        {
            ObjectMapper = objectMapper;
            CurrentTenant = currentTenant;
            UnitOfWorkManager = unitOfWorkManager;
            JobInfoRepository = jobInfoRepository;
            JobLogRepository = jobLogRepository;
        }

        public async virtual Task<List<JobInfo>> GetAllPeriodTasksAsync(CancellationToken cancellationToken = default)
        {
            var jobInfos = await JobInfoRepository.GetAllPeriodTasksAsync(cancellationToken);

            return ObjectMapper.Map<List<BackgroundJobInfo>, List<JobInfo>>(jobInfos);
        }

        public async virtual Task<List<JobInfo>> GetWaitingListAsync(
            int maxResultCount,
            CancellationToken cancellationToken = default)
        {
            var jobInfos = await JobInfoRepository.GetWaitingListAsync(maxResultCount, cancellationToken);

            return ObjectMapper.Map<List<BackgroundJobInfo>, List<JobInfo>>(jobInfos);
        }

        public async virtual Task<JobInfo> FindAsync(
            string jobId,
            CancellationToken cancellationToken = default)
        {
            return await JobInfoRepository.FindJobAsync(jobId, cancellationToken: cancellationToken);
        }

        public async virtual Task StoreAsync(
            JobInfo jobInfo,
            CancellationToken cancellationToken = default)
        {

            using var unitOfWork = UnitOfWorkManager.Begin();
            using (CurrentTenant.Change(jobInfo.TenantId))
            {
                
                //TODO: 有个bug会跳转 tenent里面
                var backgroundJobInfo = await JobInfoRepository.FindAsync(jobInfo.Id, cancellationToken: cancellationToken);
                if (backgroundJobInfo != null)
                {
                    backgroundJobInfo.SetNextRunTime(jobInfo.NextRunTime);
                    backgroundJobInfo.SetLastRunTime(jobInfo.LastRunTime);
                    backgroundJobInfo.SetStatus(jobInfo.Status);
                    backgroundJobInfo.SetResult(jobInfo.Result);
                    backgroundJobInfo.TriggerCount = jobInfo.TriggerCount;
                    backgroundJobInfo.TryCount = jobInfo.TryCount;
                    backgroundJobInfo.IsAbandoned = jobInfo.IsAbandoned;

                    await JobInfoRepository.UpdateAsync(backgroundJobInfo, cancellationToken: cancellationToken);
                }
                else
                {
                    backgroundJobInfo = new BackgroundJobInfo(
                        jobInfo.Id,
                        jobInfo.Name,
                        jobInfo.Group,
                        jobInfo.Type,
                        jobInfo.Args,
                        jobInfo.BeginTime,
                        jobInfo.EndTime,
                        jobInfo.Priority,
                        jobInfo.Source,
                        jobInfo.MaxCount,
                        jobInfo.MaxTryCount,
                        jobInfo.NodeName,
                        jobInfo.TenantId)
                    {
                        IsEnabled = true,
                        TriggerCount = jobInfo.TriggerCount,
                        IsAbandoned = jobInfo.IsAbandoned,
                        TryCount = jobInfo.TryCount,
                        LockTimeOut = jobInfo.LockTimeOut,
                        Description = jobInfo.Description
                    };
                    backgroundJobInfo.SetNextRunTime(jobInfo.NextRunTime);
                    backgroundJobInfo.SetLastRunTime(jobInfo.LastRunTime);
                    backgroundJobInfo.SetStatus(jobInfo.Status);
                    backgroundJobInfo.SetResult(jobInfo.Result);
                    switch (jobInfo.JobType)
                    {
                        case JobType.Once:
                            backgroundJobInfo.SetOnceJob(jobInfo.Interval);
                            break;
                        case JobType.Persistent:
                            backgroundJobInfo.SetPersistentJob(jobInfo.Interval);
                            break;
                        case JobType.Period:
                            backgroundJobInfo.SetPeriodJob(jobInfo.Cron);
                            break;
                    }

                    await JobInfoRepository.InsertAsync(backgroundJobInfo, cancellationToken: cancellationToken);
                }
                await unitOfWork.SaveChangesAsync(cancellationToken);
            }
        }

        public async virtual Task RemoveAsync(
            string jobId,
            CancellationToken cancellationToken = default)
        {
            using var unitOfWork = UnitOfWorkManager.Begin();
            await JobInfoRepository.DeleteAsync(jobId, cancellationToken: cancellationToken);

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async virtual Task StoreLogAsync(JobEventData eventData)
        {
            using var unitOfWork = UnitOfWorkManager.Begin();
            using (CurrentTenant.Change(eventData.TenantId))
            {
                var jogLog = new BackgroundJobLog(
                eventData.Type.Name,
                eventData.Group,
                eventData.Name,
                eventData.RunTime,
                eventData.TenantId)
                {
                    JobId = eventData.Key
                };

                jogLog.SetMessage(
                    eventData.Exception == null ? eventData.Result ?? "OK" : GetSourceException(eventData.Exception).Message,
                    eventData.Exception);

                await JobLogRepository.InsertAsync(jogLog, cancellationToken: eventData.CancellationToken);

                await unitOfWork.SaveChangesAsync(eventData.CancellationToken);
            }
        }

        public async virtual Task CleanupAsync(
            int maxResultCount,
            TimeSpan jobExpiratime,
            CancellationToken cancellationToken = default)
        {
            using var unitOfWork = UnitOfWorkManager.Begin();
            var jobs = await JobInfoRepository.GetExpiredJobsAsync(
                maxResultCount,
                jobExpiratime,
                cancellationToken);

            foreach (var item in jobs)
            {
                await JobInfoRepository.DeleteAsync(item, cancellationToken: cancellationToken);
            }

            await unitOfWork.SaveChangesAsync();
        }

        protected virtual Exception GetSourceException(Exception exception)
        {
            if (exception.InnerException != null)
            {
                return GetSourceException(exception.InnerException);
            }
            return exception;
        }
    }
}

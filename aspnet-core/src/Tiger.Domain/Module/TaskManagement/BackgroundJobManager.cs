using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger.Infrastructure.BackgroundTasks.Abstractions.Enum;
using Tiger.Infrastructure.BackgroundTasks.EventBus;
using Volo.Abp.Domain.Services;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Uow;

namespace Tiger.Module.TaskManagement
{
    /// <summary>
    /// 后台作业
    /// </summary>
    public class BackgroundJobManager: DomainService
    {
        protected IDistributedEventBus EventBus { get; }
        protected IUnitOfWorkManager UnitOfWorkManager { get; }
        protected IBackgroundJobInfoRepository BackgroundJobInfoRepository { get; }

        public BackgroundJobManager(
            IDistributedEventBus eventBus,
            IUnitOfWorkManager unitOfWorkManager,
            IBackgroundJobInfoRepository backgroundJobInfoRepository)
        {
            EventBus = eventBus;
            UnitOfWorkManager = unitOfWorkManager;
            BackgroundJobInfoRepository = backgroundJobInfoRepository;
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="jobInfo"></param>
        /// <returns></returns>
        public async virtual Task<BackgroundJobInfo> CreateAsync(BackgroundJobInfo jobInfo)
        {
            await BackgroundJobInfoRepository.InsertAsync(jobInfo);

            return jobInfo;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="jobInfo"></param>
        /// <param name="resetJob"></param>
        /// <returns></returns>
        public async virtual Task<BackgroundJobInfo> UpdateAsync(BackgroundJobInfo jobInfo, bool resetJob = false)
        {
            await BackgroundJobInfoRepository.UpdateAsync(jobInfo);

            if (!jobInfo.IsEnabled || resetJob)
            {
                UnitOfWorkManager.Current.OnCompleted(async () =>
                {
                    await EventBus.PublishAsync(
                        new JobStopEventData
                        {
                            IdList = new List<string> { jobInfo.Id },
                            TenantId = jobInfo.TenantId,
                            NodeName = jobInfo.NodeName
                        });
                });
            }

            if (resetJob && jobInfo.JobType == JobType.Period)
            {
                UnitOfWorkManager.Current.OnCompleted(async () =>
                {
                    await EventBus.PublishAsync(
                        new JobStartEventData
                        {
                            IdList = new List<string> { jobInfo.Id },
                            TenantId = jobInfo.TenantId,
                            NodeName = jobInfo.NodeName
                        });
                });
            }

            return jobInfo;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="jobInfo"></param>
        /// <returns></returns>
        public async virtual Task DeleteAsync(BackgroundJobInfo jobInfo)
        {
            await BackgroundJobInfoRepository.DeleteAsync(jobInfo);
        }

        public async virtual Task BulkDeleteAsync(IEnumerable<BackgroundJobInfo> jobInfos)
        {
            foreach (var jobInfo in jobInfos)
            {
                await BackgroundJobInfoRepository.DeleteAsync(jobInfo);
            }
            
        }

        public async virtual Task QueueAsync(BackgroundJobInfo jobInfo)
        {
            await EventBus.PublishAsync(
                new JobStartEventData
                {
                    IdList = new List<string> { jobInfo.Id },
                    TenantId = jobInfo.TenantId,
                    NodeName = jobInfo.NodeName
                });
        }

        public async virtual Task BulkQueueAsync(IEnumerable<BackgroundJobInfo> jobInfos)
        {
            if (jobInfos.Any())
            {
                await EventBus.PublishAsync(
                    new JobStartEventData
                    {
                        IdList = jobInfos.Select(x => x.Id).ToList(),
                        TenantId = jobInfos.Select(x => x.TenantId).First(),
                        NodeName = jobInfos.Select(x => x.NodeName).First()
                    });
            }
        }

        /// <summary>
        /// 触发
        /// </summary>
        /// <param name="jobInfo"></param>
        /// <returns></returns>
        public async virtual Task TriggerAsync(BackgroundJobInfo jobInfo)
        {
            await EventBus.PublishAsync(
                new JobTriggerEventData
                {
                    IdList = new List<string> { jobInfo.Id },
                    TenantId = jobInfo.TenantId,
                    NodeName = jobInfo.NodeName
                });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobInfos"></param>
        /// <returns></returns>
        public async virtual Task BulkTriggerAsync(IEnumerable<BackgroundJobInfo> jobInfos)
        {
            if (jobInfos.Any())
            {
                await EventBus.PublishAsync(
                    new JobTriggerEventData
                    {
                        IdList = jobInfos.Select(x => x.Id).ToList(),
                        TenantId = jobInfos.Select(x => x.TenantId).First(),
                        NodeName = jobInfos.Select(x => x.NodeName).First()
                    });
            }
        }

        /// <summary>
        /// 暂停
        /// </summary>
        /// <param name="jobInfo"></param>
        /// <returns></returns>
        public async virtual Task PauseAsync(BackgroundJobInfo jobInfo)
        {
            jobInfo.SetStatus(JobStatus.Paused);
            jobInfo.SetNextRunTime(null);

            await BackgroundJobInfoRepository.UpdateAsync(jobInfo);

            UnitOfWorkManager.Current.OnCompleted(async () =>
            {
                await EventBus.PublishAsync(
                    new JobPauseEventData
                    {
                        IdList = new List<string> { jobInfo.Id },
                        TenantId = jobInfo.TenantId,
                        NodeName = jobInfo.NodeName
                    });
            });
        }

        public async virtual Task BulkPauseAsync(IEnumerable<BackgroundJobInfo> jobInfos)
        {
            foreach (var jobInfo in jobInfos)
            {
                jobInfo.SetStatus(JobStatus.Paused);
                jobInfo.SetNextRunTime(null);
                await BackgroundJobInfoRepository.UpdateAsync(jobInfo);
            }

            UnitOfWorkManager.Current.OnCompleted(async () =>
            {
                await EventBus.PublishAsync(
                    new JobPauseEventData
                    {
                        IdList = jobInfos.Select(x => x.Id).ToList(),
                        TenantId = jobInfos.Select(x => x.TenantId).First(),
                        NodeName = jobInfos.Select(x => x.NodeName).First()
                    });
            });
        }

        /// <summary>
        /// 恢复
        /// </summary>
        /// <param name="jobInfo"></param>
        /// <returns></returns>
        public async virtual Task ResumeAsync(BackgroundJobInfo jobInfo)
        {
            jobInfo.SetStatus(JobStatus.Running);
            jobInfo.IsAbandoned = false;
            jobInfo.IsEnabled = true;

            await BackgroundJobInfoRepository.UpdateAsync(jobInfo);

            UnitOfWorkManager.Current.OnCompleted(async () =>
            {
                await EventBus.PublishAsync(
                    new JobResumeEventData
                    {
                        IdList = new List<string> { jobInfo.Id },
                        TenantId = jobInfo.TenantId,
                        NodeName = jobInfo.NodeName
                    });
            });
        }

        public async virtual Task BulkResumeAsync(IEnumerable<BackgroundJobInfo> jobInfos)
        {
            foreach (var jobInfo in jobInfos)
            {
                jobInfo.SetStatus(JobStatus.Running);
                jobInfo.IsAbandoned = false;
                jobInfo.IsEnabled = true;
                await BackgroundJobInfoRepository.UpdateAsync(jobInfo);
            }
            

            UnitOfWorkManager.Current.OnCompleted(async () =>
            {
                await EventBus.PublishAsync(
                    new JobResumeEventData
                    {
                        IdList = jobInfos.Select(x => x.Id).ToList(),
                        TenantId = jobInfos.Select(x => x.TenantId).First(),
                        NodeName = jobInfos.Select(x => x.NodeName).First()
                    });
            });
        }

        /// <summary>
        /// 停止
        /// </summary>
        /// <param name="jobInfo"></param>
        /// <returns></returns>
        public async virtual Task StopAsync(BackgroundJobInfo jobInfo)
        {
            jobInfo.SetStatus(JobStatus.Stopped);
            jobInfo.SetNextRunTime(null);

            await BackgroundJobInfoRepository.UpdateAsync(jobInfo);

            UnitOfWorkManager.Current.OnCompleted(async () =>
            {
                await EventBus.PublishAsync(
                    new JobStopEventData
                    {
                        IdList = new List<string> { jobInfo.Id },
                        TenantId = jobInfo.TenantId,
                        NodeName = jobInfo.NodeName
                    });
            });
        }

        public async virtual Task BulkStopAsync(IEnumerable<BackgroundJobInfo> jobInfos)
        {
            foreach (var jobInfo in jobInfos)
            {
                jobInfo.SetStatus(JobStatus.Stopped);
                jobInfo.SetNextRunTime(null);
                await BackgroundJobInfoRepository.UpdateAsync(jobInfo);
            }
            

            UnitOfWorkManager.Current.OnCompleted(async () =>
            {
                await EventBus.PublishAsync(
                    new JobStopEventData
                    {
                        IdList = jobInfos.Select(x => x.Id).ToList(),
                        TenantId = jobInfos.Select(x => x.TenantId).First(),
                        NodeName = jobInfos.Select(x => x.NodeName).First()
                    });
            });
        }
    }
}

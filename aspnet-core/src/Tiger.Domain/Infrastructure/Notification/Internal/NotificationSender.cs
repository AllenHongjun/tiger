using log4net.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger.Infrastructure.IdGenerator;
using Tiger.Infrastructure.Notification.Core;
using Tiger.Module.Notifications.Enums;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Timing;
using Volo.Abp.Uow;

namespace Tiger.Infrastructure.Notification.Internal
{
    /// <summary>
    /// 发送通知默认实现
    /// </summary>
    /// <remarks>
    /// 通过事件总线发送通知
    /// </remarks>
    public class NotificationSender : INotificationSender, ITransientDependency
    {
        public NotificationSender(IClock clock, IDistributedEventBus distributedEventBus, IDistributedIdGenerator distributedIdGenerator, IUnitOfWorkManager unitOfWorkManager, AbpNotificationsOptions options)
        {
            Clock=clock;
            DistributedEventBus=distributedEventBus;
            DistributedIdGenerator=distributedIdGenerator;
            UnitOfWorkManager=unitOfWorkManager;
            Options=options;
        }

        protected IClock Clock { get; }

        public ILogger<NotificationSender> Logger { get; set; }

        public IDistributedEventBus DistributedEventBus { get; }

        protected IDistributedIdGenerator DistributedIdGenerator { get; }

        protected IUnitOfWorkManager UnitOfWorkManager { get; }

        protected AbpNotificationsOptions Options { get; }


        public async Task<string> SendNofiterAsync(string name, NotificationData data, IEnumerable<UserIdentifier> users = null, Guid? tenantId = null, NotificationSeverity severity = NotificationSeverity.Info, IEnumerable<string> useProviders = null)
        {
            return await PublishNofiterAsync(name, data, users, tenantId, severity, useProviders);
        }

        public async Task<string> SendNofiterAsync(string name, NotificationTemplate template, IEnumerable<UserIdentifier> users = null, Guid? tenantId = null, NotificationSeverity severity = NotificationSeverity.Info, IEnumerable<string> useProviders = null)
        {
            return await PublishNofiterAsync(name, template, users, tenantId, severity, useProviders);
        }

        protected async virtual Task<string> PublishNofiterAsync<TData>(
            string name,
            TData data,
            IEnumerable<UserIdentifier> users = null,
            Guid? tenantId = null,
            NotificationSeverity severity = NotificationSeverity.Info,
            IEnumerable<string> useProviders = null)
        {
            var eto = new NotificationEto<TData>(data)
            {
                Id = DistributedIdGenerator.Create(),
                TenantId = tenantId,
                Users = users?.ToList() ?? new List<UserIdentifier>(),
                Name = name,
                CreationTime = Clock.Now,
                Severity = severity,
                UseProviders = useProviders?.ToList() ?? new List<string>()
            };

            if (UnitOfWorkManager.Current != null)
            {
                UnitOfWorkManager.Current.OnCompleted(async () =>
                {
                    await DistributedEventBus.PublishAsync(eto);
                });
            }
            else
            {
                await DistributedEventBus.PublishAsync(eto);
            }

            return eto.Id.ToString();
        }
    }
}

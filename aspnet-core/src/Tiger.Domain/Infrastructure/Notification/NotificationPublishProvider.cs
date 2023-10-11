using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Threading;

namespace Tiger.Infrastructure.Notification
{
    /// <summary>
    /// 通知发布提供者
    /// </summary>
    public abstract class NotificationPublishProvider :
        INotificationPublishProvider, ITransientDependency
    {
        public abstract string Name { get; }


        public async Task PublishAsync(
            NotificationInfo notification, 
            IEnumerable<UserIdentifier> identifiers)
        {
            if (await CanPublishAsync(notification))
            {
                await PublishAsync(notification, identifiers);
            }
        }

        protected virtual Task<bool> CanPublishAsync(
            NotificationInfo notification,
            CancellationToken cancellationToken = default)
        { 
            return Task.FromResult(true);
        }

        /// <summary>
        /// 重写实现通知发布
        /// </summary>
        /// <param name="notification"></param>
        /// <param name="identifiers"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected abstract Task PublishAsync(NotificationInfo notification, IEnumerable<UserIdentifier> identifiers, CancellationToken cancellationToken = default);
    }
}

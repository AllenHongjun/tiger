﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Volo.Abp.DependencyInjection;

namespace Tiger.Infrastructure.Notification.Internal
{
    /// <summary>
    /// 通知订阅处理器
    /// </summary>
    internal class NotificationSubscriptionManager : INotificationSubscriptionManager, ITransientDependency
    {
        private readonly INotificationStore _store;

        public NotificationSubscriptionManager(
            INotificationStore store)
        {
            _store = store;
        }

        public async virtual Task<List<NotificationSubscriptionInfo>> GetUsersSubscriptionsAsync(
            Guid? tenantId,
            string notificationName,
            IEnumerable<UserIdentifier> identifiers = null,
            CancellationToken cancellationToken = default)
        {
            return await _store.GetUserSubscriptionsAsync(tenantId, notificationName, identifiers, cancellationToken);
        }

        public async virtual Task<List<NotificationSubscriptionInfo>> GetUserSubscriptionsAsync(
            Guid? tenantId,
            Guid userId,
            CancellationToken cancellationToken = default)
        {
            return await _store.GetUserSubscriptionsAsync(tenantId, userId, cancellationToken);
        }

        public async virtual Task<List<NotificationSubscriptionInfo>> GetUserSubscriptionsAsync(
            Guid? tenantId,
            string userName,
            CancellationToken cancellationToken = default)
        {
            return await _store.GetUserSubscriptionsAsync(tenantId, userName, cancellationToken);
        }

        public async virtual Task<bool> IsSubscribedAsync(
            Guid? tenantId,
            Guid userId,
            string notificationName,
            CancellationToken cancellationToken = default)
        {
            return await _store.IsSubscribedAsync(tenantId, userId, notificationName, cancellationToken);
        }

        public async virtual Task SubscribeAsync(
            Guid? tenantId,
            UserIdentifier identifier,
            string notificationName,
            CancellationToken cancellationToken = default)
        {
            if (await IsSubscribedAsync(tenantId, identifier.UserId, notificationName, cancellationToken))
            {
                return;
            }
            await _store.InsertUserSubscriptionAsync(tenantId, identifier, notificationName, cancellationToken);
        }

        public async virtual Task SubscribeAsync(
            Guid? tenantId,
            IEnumerable<UserIdentifier> identifiers,
            string notificationName,
            CancellationToken cancellationToken = default)
        {
            foreach (var identifier in identifiers)
            {
                await SubscribeAsync(tenantId, identifier, notificationName, cancellationToken);
            }
        }

        public async virtual Task UnsubscribeAsync(
            Guid? tenantId,
            UserIdentifier identifier,
            string notificationName,
            CancellationToken cancellationToken = default)
        {
            await _store.DeleteUserSubscriptionAsync(tenantId, identifier.UserId, notificationName, cancellationToken);
        }

        public async virtual Task UnsubscribeAllAsync(
            Guid? tenantId,
            string notificationName,
            CancellationToken cancellationToken = default)
        {
            await _store.DeleteAllUserSubscriptionAsync(tenantId, notificationName, cancellationToken);
        }

        public async virtual Task UnsubscribeAsync(
            Guid? tenantId,
            IEnumerable<UserIdentifier> identifiers,
            string notificationName,
            CancellationToken cancellationToken = default)
        {
            await _store.DeleteUserSubscriptionAsync(tenantId, identifiers, notificationName, cancellationToken);
        }
        

        
    }
}

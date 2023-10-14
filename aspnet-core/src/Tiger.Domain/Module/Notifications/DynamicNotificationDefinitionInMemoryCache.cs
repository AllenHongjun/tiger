using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tiger.Infrastructure.Notification;
using Tiger.Infrastructure.Notification.Core;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Localization;

namespace Tiger.Module.Notifications
{
    /// <summary>
    /// 动态通知定义（缓存存储）
    /// </summary>
    [ExposeServices(
    typeof(IDynamicNotificationDefinitionStoreCache),
    typeof(DynamicNotificationDefinitionInMemoryCache))]
    public class DynamicNotificationDefinitionInMemoryCache : IDynamicNotificationDefinitionStoreCache, ISingletonDependency
    {
        #region 构造函数
        public string CacheStamp { get; set; }

        protected IDictionary<string, NotificationGroupDefinition> NotificationGroupDefinitions { get; }
        protected IDictionary<string, NotificationDefinition> NotificationDefinitions { get; }
        //protected ILocalizableString LocalizableStringSerializer { get; }

        public SemaphoreSlim SyncSemaphore { get; } = new(1, 1);

        public DateTime? LastCheckTime { get; set; }

        public DynamicNotificationDefinitionInMemoryCache(
            //ILocalizableString localizableStringSerializer
            )
        {
            //LocalizableStringSerializer = localizableStringSerializer;

            NotificationGroupDefinitions = new Dictionary<string, NotificationGroupDefinition>();
            NotificationDefinitions = new Dictionary<string, NotificationDefinition>();
        } 
        #endregion


        private void AddNotification(
            NotificationGroupDefinition notificationGroup,
            NotificationDefinitionRecord notificationRecord)
        {
            //TODO: 本地化改造
            var notification = notificationGroup.AddNotification(
            notificationRecord.Name,
            null, // LocalizableStringSerializer.Localize(notificationRecord.DisplayName),
            null, // LocalizableStringSerializer.Deserialize(notificationRecord.Description),
            notificationRecord.NotificationType,
            notificationRecord.NotificationLifetime,
            notificationRecord.ContentType,
            notificationRecord.AllowSubscriptionToClients
        );

            NotificationDefinitions[notification.Name] = notification;

            if (!notificationRecord.Providers.IsNullOrWhiteSpace())
            {
                notification.Providers.AddRange(notificationRecord.Providers.Split(',', ';'));
            }

            foreach (var property in notificationRecord.ExtraProperties)
            {
                notification[property.Key] = property.Value;
            }
        }


        public Task FillAsync(
            List<NotificationDefinitionGroupRecord> notificationGroupRecords, 
            List<NotificationDefinitionRecord> notificationRecords)
        {
            NotificationGroupDefinitions.Clear();
            NotificationDefinitions.Clear();

            var context = new NotificationDefinitionContext();

            foreach (var notificationGroupRecord in notificationGroupRecords)
            {
                var notificationGroup = context.AddGroup(
                    notificationGroupRecord.Name,
                    null, // LocalizableStringSerializer.Deserialize(notificationGroupRecord.DisplayName),
                    null, //LocalizableStringSerializer.Deserialize(notificationGroupRecord.Description),
                    notificationGroupRecord.AllowSubscriptionToClients
                );

                NotificationGroupDefinitions[notificationGroup.Name] = notificationGroup;

                foreach (var property in notificationGroupRecord.ExtraProperties)
                {
                    notificationGroup[property.Key] = property.Value;
                }

                var notificationRecordsInThisGroup = notificationRecords
                    .Where(p => p.GroupName == notificationGroup.Name);

                foreach (var notificationRecord in notificationRecordsInThisGroup)
                {
                    AddNotification(notificationGroup, notificationRecord);
                }
            }

            return Task.CompletedTask;
        }

        public IReadOnlyList<NotificationGroupDefinition> GetGroups()
        {
            return NotificationGroupDefinitions.Values.ToList();
        }

        public NotificationDefinition GetNotificationOrNull(string name)
        {
            return NotificationDefinitions.GetOrDefault(name);
        }

        public IReadOnlyList<NotificationDefinition> GetNotifications()
        {
            return NotificationDefinitions.Values.ToList();
        }
    }
}

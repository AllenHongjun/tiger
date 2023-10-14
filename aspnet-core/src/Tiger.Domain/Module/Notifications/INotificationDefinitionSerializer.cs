using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Infrastructure.Notification;

namespace Tiger.Module.Notifications
{
    public interface INotificationDefinitionSerializer
    {
        Task<(NotificationDefinitionGroupRecord[], NotificationDefinitionRecord[])>
            SerializeAsync(IEnumerable<NotificationGroupDefinition> NotificationGroups);

        Task<NotificationDefinitionGroupRecord> SerializeAsync(
            NotificationGroupDefinition NotificationGroup);

        Task<NotificationDefinitionRecord> SerializeAsync(
            NotificationDefinition Notification,
            [CanBeNull] NotificationGroupDefinition NotificationGroup);
    }
}

using System;
using System.Collections.Generic;
using Tiger.Module.Notifications.Enums;
using Volo.Abp.Data;

namespace Tiger.Module.Notifications
{
    public class UserNotificationInfo:IHasExtraProperties
    {
        public Guid? TenantId { get;set; }

        public string Name { get; set; }

        public long Id { get; set; }

        public Dictionary<string, object> ExtraProperties { get; set; }

        public string NotificationTypeName { get; set; }

        public DateTime CreationTime { get; set; }  

        public NotificationType Type { get; set; }

        public NotificationContentType ContentType { get; set; }

        public NotificationSeverity Severity { get; set; }

        public NotificationReadStatus ReadStatus { get; set; }

    }
}

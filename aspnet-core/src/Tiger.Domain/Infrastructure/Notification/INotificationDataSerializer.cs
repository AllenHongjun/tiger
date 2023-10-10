using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Infrastructure.Notification
{
    public interface INotificationDataSerializer
    {
        NotificationData Serialize(NotificationData notificationData);
    }
}

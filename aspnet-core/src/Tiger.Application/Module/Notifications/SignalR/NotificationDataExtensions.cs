using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Infrastructure.Notification.SignalR
{
    internal static class NotificationDataExtensions
    {
        public static NotificationData ToSignalRData(this NotificationData data)
        {
            return data;
        }
    }
}

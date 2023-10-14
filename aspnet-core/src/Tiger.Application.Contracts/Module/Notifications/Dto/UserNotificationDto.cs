using System;
using Tiger.Infrastructure.Notification;
using Tiger.Module.Notifications.Enums;

namespace Tiger.Module.Notifications.Dto;

public class UserNotificationDto
{
    public string Name { get; set; }
    public string Id { get; set; }
    public NotificationData Data { get; set; }
    public DateTime CreationTime { get; set; }
    public NotificationType Type { get; set; }
    public NotificationLifetime Lifetime { get; set; }
    public NotificationSeverity Severity { get; set; }
    public NotificationReadStatus State { get; set; }
    public NotificationContentType ContentType { get; set; }
}

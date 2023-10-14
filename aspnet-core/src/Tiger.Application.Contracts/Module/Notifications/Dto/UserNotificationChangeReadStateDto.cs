using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Tiger.Module.Notifications.Enums;

namespace Tiger.Module.Notifications.Dto;

public class UserNotificationChangeReadStateDto
{
    [Required]
    [DisplayName("Notifications:Id")]
    public long NotificationId { get; set; }

    [Required]
    [DisplayName("Notifications:State")]
    public NotificationReadStatus ReadState { get; set; } = NotificationReadStatus.Read;
}
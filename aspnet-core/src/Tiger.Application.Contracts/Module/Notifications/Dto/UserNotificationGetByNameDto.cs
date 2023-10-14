using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tiger.Module.Notifications.Dto;

public class UserNotificationGetByNameDto
{
    [Required]
    [StringLength(NotificationConsts.MaxNameLength)]
    [DisplayName("Notifications:Name")]
    public string NotificationName { get; set; }
}
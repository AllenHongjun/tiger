using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Tiger.Module.Notifications.Enums;

namespace Tiger.Module.Notifications.Dto;

public class NotificationMarkReadStateInput
{
    [Required]
    [DisplayName("Notifications:Id")]
    public long[] IdList { get; set; }

    [DisplayName("Notifications:State")]
    public NotificationReadStatus State { get; set; }
}

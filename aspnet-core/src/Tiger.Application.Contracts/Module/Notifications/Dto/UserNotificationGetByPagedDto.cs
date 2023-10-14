using System.ComponentModel;
using Tiger.Module.Notifications.Enums;
using Volo.Abp.Application.Dtos;

namespace Tiger.Module.Notifications.Dto;

public class UserNotificationGetByPagedDto : PagedAndSortedResultRequestDto
{
    public string Filter { get; set; }

    [DisplayName("Notifications:State")]
    public NotificationReadStatus? ReadState { get; set; }
}

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tiger.Module.Notifications.Dto;

public class SubscriptionsGetByNameDto
{
    [Required]
    [StringLength(NotificationConsts.MaxNameLength)]
    [DisplayName("Notifications:Name")]
    public string Name { get; set; }
}

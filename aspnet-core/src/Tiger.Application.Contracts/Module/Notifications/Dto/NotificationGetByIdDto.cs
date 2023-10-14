using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Tiger.Module.Notifications.Dto
{
    public class NotificationGetByIdDto
    {
        [Required]
        [DisplayName("Notifications:Id")]
        public long NotificationId { get; set; }
    }
}

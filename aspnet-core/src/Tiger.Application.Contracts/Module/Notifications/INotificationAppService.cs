using System.Threading.Tasks;
using Tiger.Module.Notifications.Dto;
using Volo.Abp.Application.Dtos;

namespace Tiger.Module.Notifications;

public interface INotificationAppService
{
    Task<ListResultDto<NotificationGroupDto>> GetAssignableNotifiersAsync();

    Task<ListResultDto<NotificationTemplateDto>> GetAssignableTemplatesAsync();

    Task SendAsync(NotificationSendDto input);
}

using System.Threading.Tasks;
using Tiger.Module.Notifications.Dto;
using Volo.Abp.Application.Services;

namespace Tiger.Module.Notifications;

public interface IMyNotificationAppService :
    IReadOnlyAppService<
        UserNotificationDto,
        long,
        UserNotificationGetByPagedDto
        >,
    IDeleteAppService<long>
{
    Task MarkReadStateAsync(NotificationMarkReadStateInput input);
}

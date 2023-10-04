using System.Linq;
using Volo.Abp.TextTemplating;

namespace Tiger.Infrastructure.Notification
{
    /// <summary>
    /// 通知模板定义提供者
    /// </summary>
    public class AbpNotificationTemplateDefinitionProvider : TemplateDefinitionProvider
    {
        private readonly INotificationDefinitionManager _notificationDefinitionManager;

        public AbpNotificationTemplateDefinitionProvider(
            INotificationDefinitionManager notificationDefinitionManager)
        {
            _notificationDefinitionManager = notificationDefinitionManager;
        }

        public override void Define(ITemplateDefinitionContext context)
        {
            var notifications = _notificationDefinitionManager
                .GetNotificationsAsync().GetAwaiter().GetResult();

            foreach (var notification in notifications.Where(n => n.Template != null))
            {
                context.Add(notification.Template);
            }
        }
    }
}

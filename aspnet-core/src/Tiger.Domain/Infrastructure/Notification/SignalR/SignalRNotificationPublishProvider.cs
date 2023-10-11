using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tiger.Infrastructure.Notification.SignalR
{
    public class SignalRNotificationPublishProvider : NotificationPublishProvider
    {
        public const string ProviderName = NotificationProviderNames.SignalR;

        public override string Name => throw new NotImplementedException();

        private readonly IHubContext<NotificationsHub> _hubContext;

        private readonly AbpNotificationsOptions _options;

        public SignalRNotificationPublishProvider(
            IHubContext<NotificationsHub> hubContext,
            IOptions<AbpNotificationsSignalROptions> options)
        {
            _options = options.Value;
            _hubContext = hubContext;
        }



        protected override Task PublishAsync(NotificationInfo notification, IEnumerable<UserIdentifier> identifiers, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}

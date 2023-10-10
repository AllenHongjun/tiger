using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Infrastructure.Notification
{
    public interface INotificationPublishProviderManager
    {
        List<INotificationPublishProvider> Providers { get; }
    }
}

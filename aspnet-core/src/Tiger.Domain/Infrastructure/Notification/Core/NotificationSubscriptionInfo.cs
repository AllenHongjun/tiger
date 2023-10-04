using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Infrastructure.Notification
{
    public class NotificationSubscriptionInfo
    {
        public Guid? TenantId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string NotificationName { get; set; }
    }
}

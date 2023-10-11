using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Infrastructure.Notification.SignalR.Hubs
{
    [Authorize]
    public class NotificationsHub: AbpHub
    {
    }
}

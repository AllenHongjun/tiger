using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Module.Notifications.Localization;
using Volo.Abp.Application.Services;

namespace Tiger.Module.Notifications
{
    public class AbpNotificationsApplicationServiceBase : ApplicationService
    {
        public AbpNotificationsApplicationServiceBase()
        {
            LocalizationResource = typeof(NotificationResource);
        }
    }
}

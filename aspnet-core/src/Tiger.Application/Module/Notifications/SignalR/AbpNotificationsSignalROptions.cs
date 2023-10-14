using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Infrastructure.Notification.SignalR
{
    public class AbpNotificationsSignalROptions
    {
        public AbpNotificationsSignalROptions()
        {
            MethodName = "get-notification";
        }

        /// <summary>
        /// 自定义的客户端订阅通知方法名称
        /// </summary>
        public string MethodName { get; set; }

    }
}

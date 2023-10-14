using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Module.Notifications.Enums
{
    /// <summary>
    /// 通知存货时间
    /// </summary>
    public enum NotificationLifetime
    {
        /// <summary>
        /// 持久化的
        /// </summary>
        Persistent = 0,

        /// <summary>
        /// 一次性的
        /// </summary>
        OnlyOne = 1
    }
}

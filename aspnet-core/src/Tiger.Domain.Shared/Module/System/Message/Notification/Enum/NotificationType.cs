using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Module.System.Message.Notification.Enum
{
    /// <summary>
    /// 通知类型
    /// </summary>
    public enum NotificationType
    {
        /// <summary>
        /// 应用(仅对当前租户)
        /// </summary>
        Application = 0,

        /// <summary>
        /// 系统通知(全局发布)
        /// </summary>
        System = 10,

        /// <summary>
        /// 用户通知(对应用户，受租户限制)
        /// </summary>
        User = 20
    }
}

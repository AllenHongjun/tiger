using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Module.System.Message.Notification.Enum
{
    /// <summary>
    /// 通知的严重性
    /// </summary>
    public enum NotificationSeverity
    {
        /// <summary>
        /// 成功
        /// </summary>
        Success = 0,

        /// <summary>
        /// 消息
        /// </summary>
        Info = 10,

        /// <summary>
        /// 警告
        /// </summary>
        Warning = 20,

        /// <summary>
        /// 错误
        /// </summary>
        Error = 30,

        /// <summary>
        /// 致命错误
        /// </summary>
        Fatal = 40
    }
}

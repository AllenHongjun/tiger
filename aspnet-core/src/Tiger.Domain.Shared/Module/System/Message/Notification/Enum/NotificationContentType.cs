using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Module.System.Message.Notification.Enum
{   
    /// <summary>
    /// 通知文本类型
    /// </summary>
    public enum NotificationContentType
    {   
        /// <summary>
        /// 文本
        /// </summary>
        Text = 0,

        /// <summary>
        /// Html
        /// </summary>
        Html = 1,

        /// <summary>
        /// Markdown
        /// </summary>
        Markdown = 2,

        /// <summary>
        /// Json
        /// </summary>
        Json = 3
    }
}

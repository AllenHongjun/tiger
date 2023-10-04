using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Infrastructure.Notification
{
    /// <summary>
    /// 通知内容类型
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

using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Infrastructure.Notification
{
    /// <summary>
    /// 通知定义提供者
    /// </summary>
    public interface INotificationDefinitionProvider
    {
        void Define(INotificationDefinitionContext context);
    }
}

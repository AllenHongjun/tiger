using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Module.System.Message.Notification.Enum;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;

namespace Tiger.Module.System.Message.Notification
{
    public class NotificationDefinitionRecord:BasicAggregateRoot<Guid>,IHasExtraProperties
    {   
        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 分组名称
        /// </summary>
        public virtual string GroupName { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public virtual string DisplayName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// 资源名称
        /// </summary>
        public virtual string ResourceName { get; set; }

        /// <summary>
        /// 本地化键值名称
        /// </summary>
        public virtual string Localization { get; set; }

        /// <summary>
        /// 存活类型
        /// </summary>
        public virtual NotificationLifetime Lifetime { get; set; }

        /// <summary>
        /// 通知类型
        /// </summary>
        public virtual NotificationType Type { get; set; }

        /// <summary>
        /// 通知内容类型
        /// </summary>
        public virtual NotificationContentType ContentType { get; set; }

        /// <summary>
        /// 通知提供者 多个使用;分隔
        /// </summary>
        public virtual string Providers { get; protected set; }

        /// <summary>
        /// 允许客户端订阅
        /// </summary>
        public virtual bool AllowSubscriptionToClient { get; set; }

        /// <summary>
        /// 额外属性
        /// </summary>
        public Dictionary<string, object> ExtraProperties => throw new NotImplementedException();
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Module.System.Message.Notification.Enum;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace Tiger.Module.System.Message.Notification
{
    public class UserNotificationInfo:Entity<long>,IHasCreationTime,IMultiTenant
    {   
        /// <summary>
        /// 租户Id
        /// </summary>
        public Guid? TenantId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        public string NotificationTypeName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get;set; }

        /// <summary>
        /// 类型
        /// </summary>
        public NotificationType Type { get; set; }

        /// <summary>
        /// 严重性
        /// </summary>
        public NotificationSeverity Severity { get; set; }

        /// <summary>
        /// 阅读状态
        /// </summary>
        public NotificationReadState ReadState { get; set; }
    }
}

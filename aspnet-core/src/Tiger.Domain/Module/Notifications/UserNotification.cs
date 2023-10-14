using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Tiger.Module.Notifications.Enums;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace Tiger.Module.Notifications
{

    /// <summary>
    /// 用户通知
    /// </summary>
    public class UserNotification : Entity<long>, IMultiTenant
    {
        public UserNotification()
        {
        }

        public UserNotification(long notificationId, Guid userId, Guid? tenantId = null)
        {
            UserId = userId;
            NotificationId = notificationId;
            ReadStatus = NotificationReadStatus.UnRead;
            TenantId = tenantId;
        }

        public Guid? TenantId { get; protected set; }

        public virtual Guid UserId { get; protected set; }

        /// <summary>
        /// 通知标识
        /// </summary>
        public virtual long NotificationId { get; protected set; }

        /// <summary>
        /// 阅读状态
        /// </summary>
        public virtual NotificationReadStatus ReadStatus { get; protected set; } 

        public void ChangeReadStatus(NotificationReadStatus readStatus)
        {
            ReadStatus = readStatus;
        }

    }
}

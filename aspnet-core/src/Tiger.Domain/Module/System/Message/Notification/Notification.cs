using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Module.System.Message.Notification.Enum;
using Volo.Abp.Auditing;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace Tiger.Module.System.Message.Notification
{   
    /// <summary>
    /// 通知
    /// </summary>
    public class Notification : Entity<long>, IMultiTenant, IHasCreationTime, IHasExtraProperties
    {   
        /// <summary>
        /// 组合id
        /// </summary>
        public virtual Guid? TenantId { get; protected set; }

        /// <summary>
        /// 严重性
        /// </summary>
        public virtual NotificationSeverity? NotificationSeverity { get; protected set; }

        /// <summary>
        /// 类型
        /// </summary>
        public virtual NotificationType? NotificationType { get; protected set; }

        /// <summary>
        /// 通知id
        /// </summary>
        public virtual long NotificationId { get; protected set; }

        /// <summary>
        /// 名称
        /// </summary>
        public virtual string NotificationName { get; protected set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        public virtual string NotificationTypeName { get; protected set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreationTime { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public virtual DateTime? ExpirationTime { get; set; }
        

        public Dictionary<string, object> ExtraProperties { get; protected set; }
        //public DateTime CreationTime { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        protected Notification()
        {
            ExtraProperties = new Dictionary<string, object>();
            this.SetDefaultsForExtraProperties();
        }
    }
}

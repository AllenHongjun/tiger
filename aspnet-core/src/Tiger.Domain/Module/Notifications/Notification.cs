using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Infrastructure.Notification;
using Volo.Abp.Auditing;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace Tiger.Module.Notifications
{
    public class Notification : Entity<long>, IMultiTenant, IHasCreationTime, IHasExtraProperties
    {
        public Notification()
        {
            ExtraProperties = new Dictionary<string, object>();
            this.SetDefaultsForExtraProperties();
        }

        public Notification(long id):this()
        {
            Id = id;
        }

        public Notification(long id, string name,
            string dataType,
            NotificationData data,
            NotificationSeverity severity = NotificationSeverity.Info,
            Guid? tenantId = null) : this()
        {
            NotificationId = id;
            Severity = severity;
            NotificationName = name;
            NotificationTypeName = dataType;
            Type = NotificationType.Application;
            ContentType = NotificationContentType.Text;
            TenantId = tenantId;

            SetData(data);
        }

        public void SetData(NotificationData data)
        {
            foreach (var property in data.ExtraProperties)
            {
                this.SetProperty(property.Key, property.Value);
            }
        }

        public virtual Guid? TenantId {get; protected set;}

        public virtual NotificationSeverity Severity { get; protected set;}

        public virtual NotificationType Type { get; protected set;}

        public virtual NotificationContentType ContentType { get; protected set;}

        public virtual long NotificationId { get; protected set;}

        public virtual string NotificationName { get; protected set;}   

        public virtual string NotificationTypeName { get; protected set;}

        public virtual DateTime? ExpirationTime { get; set; }

        public virtual DateTime CreationTime { get; set; }
        

        public Dictionary<string, object> ExtraProperties { get; protected set; }
    }
}

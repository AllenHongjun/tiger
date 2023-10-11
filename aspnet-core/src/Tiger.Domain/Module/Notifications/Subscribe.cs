using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace Tiger.Module.Notifications
{
    /// <summary>
    /// 订阅类
    /// </summary>
    public abstract class Subscribe : Entity<long>, IMultiTenant, IHasCreationTime
    {
        protected Subscribe()
        {
        }

        protected Subscribe(string notificationName, Guid? tenantId)
        {
            NotificationName=notificationName;
            TenantId=tenantId;
        }

        public virtual Guid? TenantId { get; protected set; }

        public virtual DateTime CreationTime { get; set; }

        public virtual string NotificationName { get; protected set; }


    }
}

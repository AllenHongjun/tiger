using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Auditing;

namespace Tiger.Module.Notifications
{
    /// <summary>
    /// 用户订阅类
    /// </summary>
    public class UserSubscribe:Subscribe, IHasCreationTime
    {
        public virtual Guid UserId { get; set; }

        public virtual string UserName { get; set;}

        protected UserSubscribe()
        {

        }

        public UserSubscribe(
            string notificationName, 
            Guid userId, 
            string userName, 
            Guid? tenantId = null)
            : base(notificationName, tenantId)
        {
            UserId = userId;
            UserName = userName;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Module.Notifications
{
    public interface INotificationDefinitionRecordRepository : IBasicRepository<NotificationDefinitionRecord, Guid>
    {
    }
}

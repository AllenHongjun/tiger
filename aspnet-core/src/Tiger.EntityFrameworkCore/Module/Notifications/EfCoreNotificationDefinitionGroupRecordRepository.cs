using System;
using System.Collections.Generic;
using System.Text;
using Tiger.EntityFrameworkCore;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Module.Notifications
{
    public class EfCoreNotificationDefinitionGroupRecordRepository :
    EfCoreRepository<TigerDbContext, NotificationDefinitionGroupRecord, Guid>,
    INotificationDefinitionGroupRecordRepository,
    ITransientDependency
    {
        public EfCoreNotificationDefinitionGroupRecordRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}

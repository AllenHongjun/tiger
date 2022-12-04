using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;

namespace Tiger.Volo.Abp.Identity
{   
    /// <summary>
    /// 组织单元仓储实现
    /// </summary>
    public class EfCoreTigerOrganizationUnitRepository : EfCoreOrganizationUnitRepository, ITigerOrganizationUnitRepository
    {
        public EfCoreTigerOrganizationUnitRepository(IDbContextProvider<IIdentityDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}

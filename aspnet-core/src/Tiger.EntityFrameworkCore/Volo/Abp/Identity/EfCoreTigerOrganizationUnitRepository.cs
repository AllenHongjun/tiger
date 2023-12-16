using System;
using System.Collections.Generic;
using System.Text;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;

namespace Tiger.Volo.Abp.Identity
{   
    /// <summary>
    /// 组织单元仓储实现
    /// </summary>
    public class EfCoreTigerOrganizationUnitRepository 
        : EfCoreRepository<TigerDbContext, OrganizationUnit, Guid>, ITigerOrganizationUnitRepository
    {
        public EfCoreTigerOrganizationUnitRepository(IDbContextProvider<TigerDbContext> dbContextProvider) 
            : base(dbContextProvider)
        {
        }
    }
}

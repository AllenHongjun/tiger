using Tiger.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;

namespace Tiger.Volo.Abp.Identity
{
    /// <summary>
    /// 组织单元仓储实现
    /// </summary>
    public class EfCoreTigerOrganizationUnitRepository 
        : EfCoreOrganizationUnitRepository, ITigerOrganizationUnitRepository
    {
        public EfCoreTigerOrganizationUnitRepository(IDbContextProvider<TigerDbContext> dbContextProvider) 
            : base(dbContextProvider)
        {
        }
    }
}

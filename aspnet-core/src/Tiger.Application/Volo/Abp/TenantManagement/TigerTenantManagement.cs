using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using Tiger.Volo.Abp.Identity;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AuditLogging;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;
using Volo.Abp.ObjectMapping;
using Volo.Abp.TenantManagement;

namespace Tiger.Volo.Abp.TenantManagement
{   
    ///// <summary>
    ///// 租户管理
    ///// </summary>
    //[RemoteService(false)]
    //[Dependency(ReplaceServices = true)]
    //[ExposeServices(typeof(ITenantAppService),
    //    typeof(TenantAppService),
    //    typeof(ITenantAppService),
    //    typeof(TenantAppService))]
    //public class TigerTenantManagement : TenantAppService, ITigerTenantAppService
    //{

    //    private readonly ITenantRepository _tenantRepository;
    //    private readonly ITenantManager _tenantManager;
    //    private readonly IDataSeeder _seeder;

    //    public TigerTenantManagement(
    //        ITenantRepository tenantRepository, 
    //        ITenantManager tenantManager, 
    //        IDataSeeder dataSeeder
    //        ) : base(tenantRepository, tenantManager, dataSeeder)
    //    {

    //        _tenantRepository = tenantRepository;
    //        _tenantManager = tenantManager;
    //        _seeder = dataSeeder;

    //    }

    //    /// <summary>
    //    /// 根据id获取租户信息
    //    /// </summary>
    //    /// <param name="id">租户id</param>
    //    /// <param name="includeDetails">includeDetails</param>
    //    /// <returns></returns>
    //    public virtual TenantDto FindById(Guid id, bool includeDetails = true)
    //    {
    //        var tenant = _tenantRepository.FindById(id, includeDetails);
    //        return ObjectMapper.Map<Tenant, TenantDto>(tenant);
    //    }

    //}
}

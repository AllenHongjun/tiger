using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using TigerAdmin.Volo.Abp.TenantManagement;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AuditLogging;
using Volo.Abp.Data;
using Volo.Abp.Identity;
using Volo.Abp.ObjectMapping;
using Volo.Abp.TenantManagement;

namespace Tiger.Volo.Abp.TenantManagement
{   
    /// <summary>
    /// 租户管理
    /// </summary>
    [RemoteService(false)]
    public class TigerTenantManagement : TenantAppService, ITigerTenantAppService
    {

        private readonly ITenantRepository _tenantRepository;
        private readonly ITenantManager _tenantManager;
        private readonly IDataSeeder _seeder;

        public TigerTenantManagement(
            ITenantRepository tenantRepository, 
            ITenantManager tenantManager, 
            IDataSeeder dataSeeder
            ) : base(tenantRepository, tenantManager, dataSeeder)
        {

            _tenantRepository = tenantRepository;
            _tenantManager = tenantManager;
            _seeder = dataSeeder;

        }


        /// <summary>
        /// 根据id获取租户信息
        /// </summary>
        /// <param name="id">租户id</param>
        /// <param name="includeDetails"></param>
        /// <returns></returns>
        public virtual TenantDto GetById(Guid id, bool includeDetails = true)
        {
            var tenant =  _tenantRepository.FindById(id, includeDetails);
            return ObjectMapper.Map<Tenant, TenantDto>(tenant);
        }



        /// <summary>
        /// 分页获取租户列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public virtual async Task<PagedResultDto<TenantDto>> GetListAsync(GetTenantDto input)
        {
            var count = await _tenantRepository.GetCountAsync();
            var list = await _tenantRepository.GetPagedListAsync(input.SkipCount,input.MaxResultCount,input.Sorting);
            return new PagedResultDto<TenantDto>(
                count,
                ObjectMapper.Map<List<Tenant>, List<TenantDto>>(list));
        }

        /// <summary>
        /// 创建租户
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual async Task<TenantDto> CreateTenant(string name)
        {
             var tenant =  await _tenantManager.CreateAsync(name);
             await CurrentUnitOfWork.SaveChangesAsync();
             return ObjectMapper.Map<Tenant, TenantDto>(tenant);
        }


        /// <summary>
        /// 修改租户名称
        /// </summary>
        /// <param name="id">租户id</param>
        /// <param name="name">租户名称</param>
        /// <returns></returns>
        public virtual async Task ChangeNameAsync(Guid id,string name)
        {
            var tenant = await _tenantRepository.GetAsync(id);
            await _tenantManager.ChangeNameAsync(tenant, name);
            await CurrentUnitOfWork.SaveChangesAsync();
        }

    }
}

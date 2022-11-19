using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Identity;
using Volo.Abp;
using Volo.Abp.TenantManagement;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Tiger.Volo.Abp.Tenant
{

    [RemoteService(Name = "tenant")]
    [Area("tenant")]
    [ControllerName("AbpTenant")]
    [Route("api/identity/tenants")]
    [ApiExplorerSettings(GroupName = "admin-erp")]
    public class TenantController : AbpController, ITenantAppService
    {   
        private readonly ITenantAppService _tenantAppService;

        public TenantController(ITenantAppService tenantAppService)
        {
            _tenantAppService=tenantAppService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<TenantDto> CreateAsync(TenantCreateDto input)
        {
            return await _tenantAppService.CreateAsync(input);
        }

        [HttpPost]
        [Route("create1")]
        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("create2")]
        public Task DeleteDefaultConnectionStringAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("create3")]
        public Task<TenantDto> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("create4")]
        public Task<string> GetDefaultConnectionStringAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("create5")]
        public Task<PagedResultDto<TenantDto>> GetListAsync(GetTenantsInput input)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("create6")]
        public Task<TenantDto> UpdateAsync(Guid id, TenantUpdateDto input)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("create7")]
        public Task UpdateDefaultConnectionStringAsync(Guid id, string defaultConnectionString)
        {
            throw new NotImplementedException();
        }
    }
}

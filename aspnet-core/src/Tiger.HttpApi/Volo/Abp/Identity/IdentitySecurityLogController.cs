using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Volo.Abp.Identity.IdentitySecurityLogs;
using Tiger.Volo.Abp.Identity.IdentitySecurityLogs.Dto;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Identity;

namespace Tiger.Volo.Abp.Identity
{
    /// <summary>
    /// 安全日志
    /// </summary>
    [RemoteService(Name = IdentityRemoteServiceConsts.RemoteServiceName)]
    [Area("identity")]
    [ControllerName("SecurityLog")]
    [ApiExplorerSettings(GroupName = "admin")]
    [Route("api/identity/security-log")]
    public class IdentitySecurityLogController : AbpController, IIdentitySecurityLogAppService
    {
        public IdentitySecurityLogController(IIdentitySecurityLogAppService identitySecurityLogAppService)
        {
            IdentitySecurityLogAppService=identitySecurityLogAppService;
        }

        protected IIdentitySecurityLogAppService IdentitySecurityLogAppService { get; }

        [HttpDelete]
        [Route("{id}")]
        public async Task DeleteAsync(Guid id)
        {
            await IdentitySecurityLogAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IdentitySecurityLogDto> GetAsync(Guid id)
        {
            return await IdentitySecurityLogAppService.GetAsync(id);
        }

        [HttpGet]
        public async Task<PagedResultDto<IdentitySecurityLogDto>> GetListAsync(GetIdentitySecurityLogInput input)
        {
            return await IdentitySecurityLogAppService.GetListAsync(input);
        }
    }
}

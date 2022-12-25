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
        protected IIdentitySecurityLogAppService IdentitySecurityLogAppService { get; }

        public IdentitySecurityLogController(IIdentitySecurityLogAppService identitySecurityLogAppService)
        {
            IdentitySecurityLogAppService=identitySecurityLogAppService;
        }

        
        /// <summary>
        /// 删除安全日志
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task DeleteAsync(Guid id)
        {
            await IdentitySecurityLogAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 获取安全日志明细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IdentitySecurityLogDto> GetAsync(Guid id)
        {
            return await IdentitySecurityLogAppService.GetAsync(id);
        }

        /// <summary>
        /// 获取安全日志列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PagedResultDto<IdentitySecurityLogDto>> GetListAsync(GetIdentitySecurityLogInput input)
        {
            return await IdentitySecurityLogAppService.GetListAsync(input);
        }
    }
}

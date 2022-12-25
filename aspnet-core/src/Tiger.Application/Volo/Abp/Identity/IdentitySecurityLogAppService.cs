using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Volo.Abp.Identity.IdentitySecurityLogs;
using Tiger.Volo.Abp.Identity.IdentitySecurityLogs.Dto;
using Tiger.Volo.Abp.IdentityServer;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace Tiger.Volo.Abp.Identity
{

    /// <summary>
    /// 安全日志服务
    /// </summary>
    [RemoteService(IsEnabled = false)]
    [Authorize(TigerIdentityPermissions.IdentitySecurityLog.Default)]
    public class IdentitySecurityLogAppService : IdentityAppServiceBase, IIdentitySecurityLogAppService
    {
        protected IIdentitySecurityLogRepository IdentitySecurityLogRepository { get; }

        protected IdentitySecurityLogManager IdentitySecurityLogManager { get; }


        public IdentitySecurityLogAppService(IIdentitySecurityLogRepository identitySecurityLogRepository)
        {
            IdentitySecurityLogRepository=identitySecurityLogRepository;
        }

        

        public async Task DeleteAsync(Guid id)
        {   
            var securityLog = await IdentitySecurityLogRepository.GetAsync(id);
            await IdentitySecurityLogRepository.DeleteAsync(securityLog);
            return;
        }

        public async Task<IdentitySecurityLogDto> GetAsync(Guid id)
        {
            var securityLog = await IdentitySecurityLogRepository.GetAsync(id);
            return ObjectMapper.Map<IdentitySecurityLog, IdentitySecurityLogDto>(securityLog);
        }

        [Authorize(TigerIdentityPermissions.IdentitySecurityLog.Default)]
        public async Task<PagedResultDto<IdentitySecurityLogDto>> GetListAsync(GetIdentitySecurityLogInput input)
        {
            var totalCount = await IdentitySecurityLogRepository.GetCountAsync(input.startTime,
                input.endTime,
                input.applicationName,
                input.identity,
                input.actionName,  // input.action 会被自动赋值,换一个名称使用
                userId:null,
                input.userName,
                input.clientId,
                input.correlationId);

            var securityLogs =  await IdentitySecurityLogRepository.GetListAsync(
                input.Sorting, 
                input.MaxResultCount, 
                input.SkipCount, 
                input.startTime, 
                input.endTime, 
                input.applicationName, 
                input.identity, 
                input.actionName, 
                userId: null, 
                input.userName, 
                input.clientId, 
                input.correlationId, 
                input.includeDetails);
            return new PagedResultDto<IdentitySecurityLogDto>(totalCount,
                ObjectMapper.Map<List<IdentitySecurityLog>, List<IdentitySecurityLogDto>>(securityLogs));
        }
    }
}

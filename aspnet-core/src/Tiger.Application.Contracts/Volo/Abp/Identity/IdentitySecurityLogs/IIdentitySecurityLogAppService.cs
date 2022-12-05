using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Volo.Abp.Identity.IdentitySecurityLogs.Dto;
using Volo.Abp.Application.Services;

namespace Tiger.Volo.Abp.Identity.IdentitySecurityLogs
{   
    /// <summary>
    /// 安全日志接口
    /// </summary>
    public interface IIdentitySecurityLogAppService: IReadOnlyAppService<IdentitySecurityLogDto, Guid, GetIdentitySecurityLogInput>, IDeleteAppService<Guid>
    {

    }
}

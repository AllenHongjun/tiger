using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Volo.Abp.IdentityServer.ApiScopes.Dto;
using Volo.Abp.Application.Services;

namespace Tiger.Volo.Abp.IdentityServer.ApiScopes
{
    public interface IApiScopeAppService :
        ICrudAppService<
            ApiScopeDto,
            Guid,
            GetApiScopeInput,
            CreateUpdateApiScopeDto>
    {
    }
}

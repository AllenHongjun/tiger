﻿using System;
using System.Threading.Tasks;
using Tiger.Volo.Abp.IdentityServer.ApiScopes;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Volo.Abp.IdentityServer
{
    /// <summary>
    /// Api作用域
    /// </summary>
    /// <remarks>
    /// 3.2 版本id4 没有这个功能
    /// </remarks>
    [RemoteService(false)]
    public class ApiScopeAppService : ApplicationService, IApiScopeAppService
    {
        // TODO:  实现 apiScope的仓储
        //protected IApiScopeRepository ApiScopeRepository { get; }

        public Task<ApiScopeDto> CreateAsync(ApiScopeCreateDto input)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiScopeDto> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResultDto<ApiScopeDto>> GetListAsync(GetApiScopeInput input)
        {
            throw new NotImplementedException();
        }

        public Task<ApiScopeDto> UpdateAsync(Guid id, ApiScopeUpdateDto input)
        {
            throw new NotImplementedException();
        }
    }
}

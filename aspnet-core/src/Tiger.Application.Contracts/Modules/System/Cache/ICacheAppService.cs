using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Tiger.Modules.System.Cache
{   
    /// <summary>
    /// 缓存管理接口
    /// </summary>
    public interface ICacheAppService:IApplicationService
    {
        Task<CacheKeysDto> GetCacheKeysAsync(GetCacheKeysInput input);

        Task<CacheValueDto> GetValueAsync(CacheKeyInput input);

        Task RefreshAsync(CacheRefreshInput input);

        Task RemoveAsync(CacheKeyInput input);
    }
}

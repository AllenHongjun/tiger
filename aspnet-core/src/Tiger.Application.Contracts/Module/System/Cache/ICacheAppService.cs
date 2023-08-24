using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Tiger.Module.System.Cache
{
    /// <summary>
    /// 缓存管理接口
    /// </summary>
    public interface ICacheAppService : IApplicationService
    {
        /// <summary>
        /// 获取基本信息
        /// </summary>
        /// <returns></returns>
        Task<Dictionary<string, string>> GetBasicInfo();

        Task<CacheKeysDto> GetCacheKeysAsync(GetCacheKeysInput input);

        Task<CacheValueDto> GetValueAsync(CacheKeyInput input);

        Task RefreshAsync(CacheRefreshInput input);

        Task RemoveAsync(CacheKeyInput input);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tiger.Module.System.Cache
{   
    /// <summary>
    /// 缓存管理接口
    /// </summary>
    public interface ICacheManager
    {   
        /// <summary>
        /// 获取缓存key
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<CackeKeysResponse> GetKeysAsync(GetCacheKeyRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// 获取缓存值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<CacheValueResponse> GetValueAsync(string key, CancellationToken cancellationToken = default);

        /// <summary>
        /// 刷新缓存
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task RefreshAsync(RefreshCacheRequest request, CancellationToken cancellationToken = default);

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task RemoveAsync(string key, CancellationToken cancellationToken = default);
    }
}

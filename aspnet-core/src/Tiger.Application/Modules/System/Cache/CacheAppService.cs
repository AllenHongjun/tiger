using Microsoft.AspNetCore.Authorization;
using Quartz.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tiger.Module.System.Cache;
using Tiger.Modules.System.Cache.Localization;
using Tiger.Modules.System.Cache.Permissions;
using Volo.Abp;
using Volo.Abp.Application.Services;

namespace Tiger.Modules.System.Cache
{   
    /// <summary>
    /// 缓存管理服务
    /// </summary>
    [Authorize(CachingManagementPermissionNames.Cache.Default)]
    [RemoteService(IsEnabled = false)]
    public class CacheAppService : ApplicationService, ICacheAppService
    {
        public CacheAppService(ICacheManager cacheManager)
        {
            CacheManager=cacheManager;
            LocalizationResource = typeof(CacheResource);
        }

        protected ICacheManager CacheManager { get; }
        
        public async Task<CacheKeysDto> GetCacheKeysAsync(GetCacheKeysInput input)
        {
            var res = await CacheManager.GetKeysAsync(
                new GetCacheKeyRequest(input.Prefix,input.Filter,input.Marker));

            return new CacheKeysDto
            {
                NextMarker = res.NextMarker,
                Keys = res.Keys.ToList(),
            };
        }

        public async Task<CacheValueDto> GetValueAsync(CacheKeyInput input)
        {
            var res = await CacheManager.GetValueAsync(input.Key);

            var value = new CacheValueDto
            {
                Size = res.Size,
                Type = res.Type,
                Values = res.Values,
            };

            if (res.Ttl.HasValue)
            {
                value.Expiration = Clock.Now.Add(res.Ttl.Value);
            }

            return value;
        }

        [Authorize(CachingManagementPermissionNames.Cache.Refresh)]
        public async virtual Task RefreshAsync(CacheRefreshInput input)
        {
            TimeSpan? absExpir = null;
            TimeSpan? sldExpr = null;
            if (input.AbsoluteExpiration.HasValue && input.AbsoluteExpiration.Value > Clock.Now)
            {
                absExpir = input.AbsoluteExpiration.Value - Clock.Now;
            }
            if (input.SlidingExpiration.HasValue && input.SlidingExpiration.Value > Clock.Now)
            {
                sldExpr = input.SlidingExpiration.Value - Clock.Now;
            }

            await CacheManager.RefreshAsync(new RefreshCacheRequest(input.Key, absExpir, sldExpr));

        }

        [Authorize(CachingManagementPermissionNames.Cache.Delete)]
        public async Task RemoveAsync(CacheKeyInput input)
        {
            await CacheManager.RemoveAsync(input.Key);
        }
    }
}

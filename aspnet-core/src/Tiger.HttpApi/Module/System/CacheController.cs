﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Module.System.Cache;
using Tiger.Module.System.Cache.Localization;
using Tiger.Module.System.Cache.Permissions;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;

namespace Tiger.Module.System
{
    /// <summary>
    /// 缓存管理
    /// </summary>
    [Controller]
    [Authorize(CachingManagementPermissionNames.Cache.Default)]
    [RemoteService(Name = AbpCachRemoteServiceConsts.RemoteServiceName)]
    [Route("api/caching-management/cache")]
    public class CacheController : AbpController, ICacheAppService
    {
        protected ICacheAppService CacheAppService { get; }

        public CacheController(ICacheAppService cacheAppService)
        {
            CacheAppService = cacheAppService;

            LocalizationResource = typeof(CacheResource);
        }

        /// <summary>
        /// 获取基本信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("info")]
        public Task<Dictionary<string, string>> GetBasicInfo()
        {
            return CacheAppService.GetBasicInfo();
        }


        [HttpGet]
        [Route("keys")]
        public virtual Task<CacheKeysDto> GetCacheKeysAsync(GetCacheKeysInput input)
        {
            return CacheAppService.GetCacheKeysAsync(input);
        }

        [HttpGet]
        [Route("values")]
        public virtual Task<CacheValueDto> GetValueAsync(CacheKeyInput input)
        {
            return CacheAppService.GetValueAsync(input);
        }

        [HttpPut]
        [Route("refresh")]
        public virtual Task RefreshAsync(CacheRefreshInput input)
        {
            return CacheAppService.RefreshAsync(input);
        }

        [HttpDelete]
        [Route("delete")]
        public virtual Task RemoveAsync(CacheKeyInput input)
        {
            return CacheAppService.RemoveAsync(input);
        }

        
    }
}

using AutoMapper;
using JetBrains.Annotations;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;
using Volo.Abp.ObjectMapping;

namespace Tiger.Volo.Abp.Sass.Editions
{   
    /// <summary>
    /// 版本缓存操作类
    /// </summary>
    public class EditionStore : IEditionStore, ITransientDependency
    {
        public EditionStore(
            IEditionRepository editionRepository, 
            IObjectMapper<TigerDomainModule> objectMapper, 
            ICurrentTenant currentTenant, 
            IDistributedCache<EditionCacheItem> editionCache)
        {
            EditionRepository=editionRepository;
            ObjectMapper=objectMapper;
            CurrentTenant=currentTenant;
            EditionCache=editionCache;
        }

        protected IEditionRepository EditionRepository { get; }
        protected IObjectMapper<TigerDomainModule> ObjectMapper { get; }
        protected ICurrentTenant CurrentTenant { get; }
        protected IDistributedCache<EditionCacheItem> EditionCache { get; }

        
        public async virtual Task<EditionInfo> FindByTenantAsync(Guid tenantId)
        {
            return (await GetCacheItemAsync(tenantId)).Value;
        }

        /// <summary>
        /// 获取缓存的值
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        protected async virtual Task<EditionCacheItem> GetCacheItemAsync(Guid tenantId)
        {   
            // 计算租户版本的缓存键值
            var cacheKey = CalculateCacheKey(tenantId);
            // 读取缓存
            var cacheItem = await EditionCache.GetAsync(cacheKey, considerUow: true);

            if (cacheItem != null) {
                return cacheItem;
            }
            else
            {
                using (CurrentTenant.Change(null))
                {
                    var editon = await EditionRepository.FindByTenantIdAsync(tenantId);
                    return await SetCacheAsync(cacheKey, editon);
                }
            }
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="edition"></param>
        /// <returns></returns>
        protected async virtual Task<EditionCacheItem> SetCacheAsync(string cacheKey, [CanBeNull] Edition edition)
        {   
            // 最好不要将数据库的实体类直接保存到缓存当中
            var editionInfo = edition != null ? ObjectMapper.Map<Edition, EditionInfo>(edition) : null;
            var cacheItem = new EditionCacheItem(editionInfo);
            await EditionCache.SetAsync(cacheKey, cacheItem, considerUow:true);
            return cacheItem;
        }





        /// <summary>
        /// 计算缓存的键值
        /// </summary>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        /// <remarks>
        /// 给每一个缓存对象建立一个键名方便查找
        /// </remarks>
        protected virtual string CalculateCacheKey(Guid tenantId)
        {
            return EditionCacheItem.CalculateCacheKey(tenantId);
        }

        
    }
}

using Tiger.Volo.Abp.Sass.Editions;
using System.Threading.Tasks;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EventBus;

namespace Tiger.Volo.Abp.Sass.Tenants;

public class TenantCacheItemInvalidator : ILocalEventHandler<EntityChangedEventData<Tenant>>, ITransientDependency
{
    protected IDistributedCache<TenantCacheItem> Cache { get; }

    public TenantCacheItemInvalidator(IDistributedCache<TenantCacheItem> cache)
    {
        Cache = cache;
    }

    public async virtual Task HandleEventAsync(EntityChangedEventData<Tenant> eventData)
    {
        var keys = new string[]
        {
            TenantCacheItem.CalculateCacheKey(null, eventData.Entity.Name),
            TenantCacheItem.CalculateCacheKey(eventData.Entity.Id, null),
            TenantCacheItem.CalculateCacheKey(eventData.Entity.Id, eventData.Entity.Name),

            // 同时移除租户版本缓存
            EditionCacheItem.CalculateCacheKey(eventData.Entity.Id)
        };

        await Cache.RemoveAsync(TenantCacheItem.CalculateCacheKey(null, eventData.Entity.Name), considerUow: true);
        await Cache.RemoveAsync(TenantCacheItem.CalculateCacheKey(eventData.Entity.Id, null));
        await Cache.RemoveAsync(TenantCacheItem.CalculateCacheKey(eventData.Entity.Id, eventData.Entity.Name));
        await Cache.RemoveAsync(EditionCacheItem.CalculateCacheKey(eventData.Entity.Id));

        // 3.2.1 版本没有移除多个的方法
        //await Cache.RemoveManyAsync(keys, considerUow: true);
    }
}

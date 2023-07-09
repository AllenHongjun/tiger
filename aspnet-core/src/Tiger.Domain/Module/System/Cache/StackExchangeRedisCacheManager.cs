﻿using Microsoft.Extensions.Caching.StackExchangeRedis;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using Volo.Abp;

namespace Tiger.Module.System.Cache
{
    /// <summary>
    /// StackExchangeRedis缓存管理实现
    /// </summary>
    //[Dependency(ReplaceServices = true)]
    public class StackExchangeRedisCacheManager : ICacheManager, ISingletonDependency
    {
        private readonly static MethodInfo GetRedisDatabaseMethod;
        private readonly static MethodInfo ConnectAsyncMethod;

        protected RedisCacheOptions RedisCacheOptions { get; }
        protected AbpDistributedCacheOptions CacheOptions { get; }
        protected ICurrentTenant CurrentTenant { get; }
        protected IDistributedCache DistributedCache { get; }
        protected AbpRedisCache RedisCache => DistributedCache.As<AbpRedisCache>();

        private IDatabase RedisDatabase => GetRedisDatabase();

        private IDatabase _redisDatabase;

        public StackExchangeRedisCacheManager(
            RedisCacheOptions redisCacheOptions, 
            AbpDistributedCacheOptions cacheOptions, 
            ICurrentTenant currentTenant, 
            IDistributedCache distributedCache)
        {
            RedisCacheOptions=redisCacheOptions;
            CacheOptions=cacheOptions;
            CurrentTenant=currentTenant;
            DistributedCache=distributedCache;
        }

        static StackExchangeRedisCacheManager()
        {
            var type = typeof(AbpRedisCache);
            ConnectAsyncMethod = type.GetMethod("ConnectAsync", BindingFlags.Instance | BindingFlags.NonPublic);
            GetRedisDatabaseMethod = type.GetMethod("GetRedisDatabase", BindingFlags.Instance | BindingFlags.NonPublic);
        }

        /// <summary>
        /// 获取所有缓存key
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<CackeKeysResponse> GetKeysAsync(GetCacheKeyRequest request, CancellationToken cancellationToken = default)
        {
            await ConnectAsync(cancellationToken);

            // 缓存键名规则: InstanceName + (t + TenantId)(CurrentTenant.IsAvailable) + CacheItemName + KeyPrefix + Key
            // 缓存键名规则: InstanceName + (c:)(!CurrentTenant.IsAvailable) + CacheItemName + KeyPrefix + Key

            var match = "*";
            // abp*
            if (!RedisCacheOptions.InstanceName.IsNullOrWhiteSpace())
            {
                match = RedisCacheOptions.InstanceName;
            }
            // abp*t:xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx*
            // abp*c:*
            if (CurrentTenant.IsAvailable)
            {
                match += "t:" + CurrentTenant.Id.ToString() + "*";
            }
            else
            {
                match += "c:*";
            }
            // abp*t:xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx*application*
            // abp*c:*application*
            if (!CacheOptions.KeyPrefix.IsNullOrWhiteSpace())
            {
                match += CacheOptions.KeyPrefix + "*";
            }

            // app*abp*t:xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx*application*
            // app*abp*c:*application*
            if (!request.Prefix.IsNullOrWhiteSpace())
            {
                match = request.Prefix + "*" + match;
            }

            // if filter is Mailing:
            // app*abp*t:xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx*application*Mailing*
            // app*abp*c:*application*Mailing*
            if (!request.Filter.IsNullOrWhiteSpace())
            {
                match += request.Filter + "*";
            }
            // scan 0 match * count 50000
            // redis有自定义的key排序,由传递的marker来确定下一次检索起始位

            var args = new object[] { request.Marker ?? "0", "match", match, "count", 50000 };

            var result = await RedisDatabase.ExecuteAsync("scan", args);

            var results = (RedisResult[])result;

            // 第一个返回结果 下一次检索起始位 0复位
            // 第二个返回结果为key列表
            // https://redis.io/commands/scan/
            return new CackeKeysResponse(
                (string)results[0],
                (string[])results[1]);
        }

        public async Task<CacheValueResponse> GetValueAsync(string key, CancellationToken cancellationToken = default)
        {
            await ConnectAsync(cancellationToken);

            long size = 0;
            var values = new Dictionary<string, object>();

            // type RedisKey
            var type = await RedisDatabase.KeyTypeAsync(key);
            // ttl RedisKey
            var ttl = await RedisDatabase.KeyTimeToLiveAsync(key);

            switch (type)
            {
                case RedisType.Hash:
                    // hlen RedisKey
                    size = await RedisDatabase.HashLengthAsync(key);
                    // hscan RedisKey
                    var hvalues = RedisDatabase.HashScan(key);
                    foreach (var hvalue in hvalues)
                    {
                        if (!hvalue.Name.IsNullOrEmpty)
                        {
                            values.Add(hvalue.Name.ToString(), hvalue.Value.IsNullOrEmpty ? "" : hvalue.Value.ToString());
                        }
                    }
                    break;
                case RedisType.String:
                    // strlen RedisKey
                    size = await RedisDatabase.StringLengthAsync(key);
                    // get RedisKey
                    var svalue = RedisDatabase.StringGet(key);
                    values.Add("value", svalue.IsNullOrEmpty ? "" : svalue.ToString());
                    break;
                case RedisType.List:
                    // llen RedisKey
                    size = await RedisDatabase.ListLengthAsync(key);
                    // lrange RedisKey
                    var lvalues = RedisDatabase.ListRange(key);
                    for (var lindex = 0; lindex < lvalues.Length; lindex++)
                    {
                        if (!lvalues[lindex].IsNullOrEmpty)
                        {
                            values.Add($"index.{lindex}", lvalues[lindex].IsNullOrEmpty ? "" : lvalues[lindex].ToString());
                        }
                    }
                    break;
                default:
                    throw new BusinessException("Abp.CachingManagement:01001")
                        .WithData("Type", type.ToString());
            }

            return new CacheValueResponse(
                type.ToString(),
                size,
                ttl,
                values
                );
        }

        /// <summary>
        /// 刷新缓存
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task RefreshAsync(RefreshCacheRequest request, CancellationToken cancellationToken = default)
        {
            var cacheKey = request.Key;
            if (!RedisCacheOptions.InstanceName.IsNullOrWhiteSpace() && cacheKey.StartsWith(RedisCacheOptions.InstanceName))
            {
                cacheKey = cacheKey.Substring(RedisCacheOptions.InstanceName.Length);
            }
            if (request.AbsoluteExpiration.HasValue || request.SlidingExpiration.HasValue)
            {
                var value = await RedisCache.GetAsync(cacheKey, cancellationToken);

                await RedisCache.SetAsync(cacheKey, value,
                    new DistributedCacheEntryOptions
                    {
                        SlidingExpiration = request.SlidingExpiration,
                        AbsoluteExpirationRelativeToNow = request.AbsoluteExpiration,
                    },
                    cancellationToken);

                return;
            }
        }

        public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
        {
            var cacheKey = key;
            if (!RedisCacheOptions.InstanceName.IsNullOrWhiteSpace() && cacheKey.StartsWith(RedisCacheOptions.InstanceName))
            {
                cacheKey = cacheKey.Substring(RedisCacheOptions.InstanceName.Length);
            }

            await RedisCache.RemoveAsync(cacheKey, cancellationToken);
        }


        protected virtual Task ConnectAsync(CancellationToken cancellationToken = default)
        {
            if(_redisDatabase != null)
            {
                return Task.CompletedTask;
            }

            return (Task)ConnectAsyncMethod.Invoke(RedisCache, new object[] { cancellationToken });
        }


        private IDatabase GetRedisDatabase()
        {
            if (_redisDatabase == null)
            {
                _redisDatabase = GetRedisDatabaseMethod.Invoke(RedisCache, null) as IDatabase;
            }

            return _redisDatabase;
        }

    }
}

/**
 * 类    名：RedisCachingExtensions   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/15 23:32:23       
 * 说    明: 
 * 
 */

using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Utility;

namespace Tiger.Cache
{
    public static class RedisCachingExtensions
    {
        /// <summary>
        /// 获取或添加缓存
        /// </summary>
        /// <typeparam name="TCacheItem"></typeparam>
        /// <param name="cache"></param>
        /// <param name="key"></param>
        /// <param name="factory"></param>
        /// <param name="minutes"></param>
        /// <returns></returns>
        public static async Task<TCacheItem> GetOrAddAsync<TCacheItem>(this IDistributedCache cache, string key, Func<Task<TCacheItem>> factory, int minutes)
        {
            TCacheItem cacheItem;

            var result = await cache.GetStringAsync(key);
            if (string.IsNullOrEmpty(result))
            {
                cacheItem = await factory.Invoke();

                var options = new DistributedCacheEntryOptions();
                if (minutes != CacheStrategy.NEVER)
                {
                    options.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(minutes);
                }
               
                await cache.SetStringAsync(key, JsonHelper.SerializeObject(cacheItem), options);
                return cacheItem;
            }
            else
            {
                //cacheItem = JsonHelper.DeserializeJsonToObject<TCacheItem>(result);// 
            }
            return default(TCacheItem);
        }
    }
}
}

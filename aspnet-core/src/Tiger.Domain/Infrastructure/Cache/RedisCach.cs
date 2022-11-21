/**
 * 类    名：RedisCach   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/16 0:54:02       
 * 说    明: 
 * 
 */

using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Utilities;
using Volo.Abp.Caching;

namespace Tiger.Cache
{
    public class RedisCach
    {
        private readonly IDistributedCache<string> _cache;

        public RedisCach(IDistributedCache<string> cache)
        {
            _cache = cache;
        }


        public Task<string> GetOrAddAsync(string key, Func<Task<string>> factory, Func<DistributedCacheEntryOptions> optionsFactory = null )
        {
            var res = factory();
            JsonHelper.SerializeObject(res);

            _cache.GetOrAddAsync(key, factory, null);

            return null;
        }
    }
}

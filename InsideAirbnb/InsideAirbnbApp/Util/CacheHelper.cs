using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;

namespace InsideAirbnbApp.Util
{
    public class CacheHelper
    {
        private readonly IDistributedCache _cache;
        private readonly DistributedCacheEntryOptions _cacheOptions;

        public CacheHelper(IDistributedCache cache)
        {
            _cache = cache;
            _cacheOptions = new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromMinutes(1),
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            };
        }

        public async Task<string> Get(string key)
        {
            var item = await _cache.GetAsync(key);
            return item == null ? null : Encoding.ASCII.GetString(item);
        }

        public void Set(string key, string response)
        {
            _cache.SetAsync(key, Encoding.ASCII.GetBytes(response), _cacheOptions);
        }
    }
}

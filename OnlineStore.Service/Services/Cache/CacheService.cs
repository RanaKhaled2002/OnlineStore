using OnlineStore.Core.Services.Contract.Cache;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OnlineStore.Service.Services.Cache
{
    public class CacheService : ICacheService
    { 
        private readonly IDatabase _database;
        public CacheService(IConnectionMultiplexer redis) 
        { 
            _database = redis.GetDatabase();
        }

        public async Task<string> GetCacheKeyAsync(string key)
        {
            var cacheRsponse = await _database.StringGetAsync(key);
            if (cacheRsponse.IsNullOrEmpty) return null;
            return cacheRsponse.ToString();
        }

        public async Task SetCahceKeyAsync(string key, object response, TimeSpan expireDate)
        {
            if (response is null) return;

            var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            await _database.StringSetAsync(key,JsonSerializer.Serialize(response,options),expireDate);
        }
    }
}

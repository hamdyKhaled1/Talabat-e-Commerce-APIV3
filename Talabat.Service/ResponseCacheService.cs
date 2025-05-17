using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Service;

namespace Talabat.Service
{
    public class ResponseCacheService : IResponseCacheService
    {
        private readonly StackExchange.Redis.IDatabase _database;
        public ResponseCacheService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task  CacheResponseAsync(string cacheKey, object response, TimeSpan timeToLive)
        {
            if (response == null) return;
            var options=new JsonSerializerOptions() { PropertyNamingPolicy=JsonNamingPolicy.CamelCase };
            var serializerResponse=JsonSerializer.Serialize(response,options);
            await _database.StringSetAsync(cacheKey,serializerResponse,timeToLive);


        }

        public async Task<string> GetCachedResponseAsync(string cacheKey)
        {
            var CachedResponse=await _database.StringGetAsync(cacheKey);
            if (CachedResponse.IsNullOrEmpty) return null;
            return CachedResponse;
        }
    }
}

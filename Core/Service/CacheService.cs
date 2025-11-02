using DomainLayer.Contracts;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Service
{
    public class CacheService(ICasheRepository casheRepository) : ICacheService
    {
        public async Task<string?> GetAsync(string cacheKey)
        {
            return await casheRepository.GetAsync(cacheKey);
        }

        public async Task SetAsync(string cacheKey, object cacheValue, TimeSpan timeToLive)
        {
            var value = JsonSerializer.Serialize(cacheValue);
             await casheRepository.SetAsync(cacheKey, value, timeToLive);
        }
    }
}

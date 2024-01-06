using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Application.API.Config
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _cache;
        private readonly DistributedCacheEntryOptions _options;

        public CacheService(IDistributedCache cache)
        {
            _cache = cache;
            _options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60), // define o tempo de expiração
                SlidingExpiration = TimeSpan.FromSeconds(20), //O tempo em que o objeto será removido do cache se ele não foi acessado
            };
        }

        public async Task<string> GetAsync(string key)
        {
            return _cache.GetString(key);
        }

        public async Task SetAsync(string key, string value)
        {
            await _cache.SetStringAsync(key, value, _options);
        }

        public  async Task SetAsync(
        string key, //DEFINE O KEY DO REGISTRO
        object data, //DEFINE O TIPO DE DADO
        DistributedCacheEntryOptions options) // DEFINE O TEMPO DE INATIVIDADE
        {
            //serialize the data
            var jsonData = JsonConvert.SerializeObject(data);

            //save the data in redis cache
            await _cache.SetStringAsync(key, jsonData, options);
        }
    }
}

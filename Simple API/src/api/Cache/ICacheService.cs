using Microsoft.Extensions.Caching.Distributed;
using System.Threading.Tasks;

namespace Application.API.Config
{
    public interface ICacheService
    {
        Task SetAsync(string key, string value);
        Task SetAsync(string key, object data, DistributedCacheEntryOptions options);
        Task<string> GetAsync(string key);

    }
}

namespace Application.API.Config
{
    public interface ICacheService
    {
        Task SetAsync(string key, string value);
        Task<string> GetAsync(string key);
    }
}

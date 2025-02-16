using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioWave.Services
{
    public interface ICacheService
    {
        Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> fetchData, TimeSpan? expiration = null);
        Task ClearAsync(string key);
        Task ClearAllAsync();
    }
}

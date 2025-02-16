using RadioWave.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RadioWave.Services
{
    public class CacheService : ICacheService
    {
        private readonly string _cacheFolder;
        private const int DEFAULT_CACHE_HOURS = 24;

        public CacheService()
        {
            _cacheFolder = Path.Combine(FileSystem.CacheDirectory, "RadioCache");
            if (!Directory.Exists(_cacheFolder))
            {
                Directory.CreateDirectory(_cacheFolder);
            }
        }

        public async Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> fetchData, TimeSpan? expiration = null)
        {
            var filePath = GetFilePath(key);
            var expirationTime = expiration ?? TimeSpan.FromHours(DEFAULT_CACHE_HOURS);

            try
            {
                if (File.Exists(filePath))
                {
                    var json = await File.ReadAllTextAsync(filePath);
                    var cacheEntry = JsonSerializer.Deserialize<CacheEntry<T>>(json);

                    if (cacheEntry.ExpirationTime > DateTime.UtcNow)
                    {
                        return cacheEntry.Data;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Cache read error: {ex.Message}");
            }

            // Fetch new data
            var data = await fetchData();

            // Save to cache
            try
            {
                var cacheEntry = new CacheEntry<T>
                {
                    Data = data,
                    ExpirationTime = DateTime.UtcNow.Add(expirationTime)
                };

                var json = JsonSerializer.Serialize(cacheEntry);
                await File.WriteAllTextAsync(filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Cache write error: {ex.Message}");
            }

            return data;
        }

        public async Task ClearAsync(string key)
        {
            var filePath = GetFilePath(key);
            if (File.Exists(filePath))
            {
                await Task.Run(() => File.Delete(filePath));
            }
        }

        public async Task ClearAllAsync()
        {
            try
            {
                var files = Directory.GetFiles(_cacheFolder);
                foreach (var file in files)
                {
                    await Task.Run(() => File.Delete(file));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Cache clear error: {ex.Message}");
            }
        }

        private string GetFilePath(string key)
        {
            return Path.Combine(_cacheFolder, $"{key}.json");
        }
    }
}

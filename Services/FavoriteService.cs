using RadioWave.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RadioWave.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly string _favoriteStationsKey = "FavoriteStations";

        public async Task<List<RadioStation>> GetFavoriteStationsAsync()
        {
            var favorites = await SecureStorage.GetAsync(_favoriteStationsKey);
            return string.IsNullOrEmpty(favorites)
                ? new List<RadioStation>()
                : JsonSerializer.Deserialize<List<RadioStation>>(favorites);
        }

        public async Task AddFavoriteStationAsync(RadioStation station)
        {
            var favorites = await GetFavoriteStationsAsync();
            favorites.Add(station);
            await SecureStorage.SetAsync(_favoriteStationsKey, JsonSerializer.Serialize(favorites));
        }

        public async Task RemoveFavoriteStationAsync(string stationId)
        {
            var favorites = await GetFavoriteStationsAsync();
            favorites.RemoveAll(x => x.Stationuuid == stationId);
            await SecureStorage.SetAsync(_favoriteStationsKey, JsonSerializer.Serialize(favorites));
        }

        public async Task<bool> IsFavoriteAsync(string stationId)
        {
            var favorites = await GetFavoriteStationsAsync();
            return favorites.Any(x => x.Stationuuid == stationId);
        }
    }
}

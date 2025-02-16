using RadioWave.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioWave.Services
{
    public interface IFavoriteService
    {
        Task<List<RadioStation>> GetFavoriteStationsAsync();
        Task AddFavoriteStationAsync(RadioStation station);
        Task RemoveFavoriteStationAsync(string stationId);
        Task<bool> IsFavoriteAsync(string stationId);
    }
}

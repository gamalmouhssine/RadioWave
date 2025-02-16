using RadioWave.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioWave.Services
{
    public interface IRadioService
    {
        Task<List<Country>> GetCountriesAsync();
        Task<List<RadioStation>> GetStationsByCountryAsync(string country ,int page = 0);
    }
}

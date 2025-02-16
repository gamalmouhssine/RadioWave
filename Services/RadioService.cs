using RadioWave.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RadioWave.Services
{
    public class RadioService : IRadioService
    {
        private readonly HttpClient _httpClient;
        private readonly ICacheService _cacheService;
        private const string BaseUrl = "https://de1.api.radio-browser.info";
        private const int PageSize = 50;

        public RadioService(ICacheService cacheService)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "MAUI Radio App");
            _cacheService = cacheService;
        }

        public async Task<List<Country>> GetCountriesAsync()
        {
            return await _cacheService.GetOrSetAsync("countries", async () =>
            {
                try
                {
                    var response = await _httpClient.GetAsync($"{BaseUrl}/json/countries");
                    response.EnsureSuccessStatusCode();
                    var content = await response.Content.ReadAsStringAsync();
                    var countries = JsonSerializer.Deserialize<List<Country>>(content, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                    return countries?.OrderByDescending(c => c.Stationcount).ToList() ?? new List<Country>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error fetching countries: {ex.Message}");
                    throw;
                }
            });
        }

        public async Task<List<RadioStation>> GetStationsByCountryAsync(string country, int page = 0)
        {
            try
            {
                var response = await _httpClient.GetAsync(
                    $"{BaseUrl}/json/stations/bycountry/{Uri.EscapeDataString(country)}?" +
                    $"offset={page * PageSize}&limit={PageSize}&hidebroken=true&order=clickcount&reverse=true"
                );
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                var stations = JsonSerializer.Deserialize<List<RadioStation>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<RadioStation>();

                // Manually add MedRadio for Morocco
                if (country.Equals("Morocco", StringComparison.OrdinalIgnoreCase))
                {
                    var medRadio = new RadioStation
                    {
                        Stationuuid = Guid.NewGuid().ToString(), // Generate a unique ID
                        Name = "MedRadio",
                        Url = "https://medradio.ice.infomaniak.ch/medradio-128.mp3",
                        Url_resolved = "https://medradio.ice.infomaniak.ch/medradio-128.mp3",
                        Favicon = "https://medradio.ma/wp-content/uploads/2023/07/Med_logo_white.png", 
                        Country = "Morocco",
                        Countrycode = "MA",
                        
                    };

                    // Add to the beginning of the list
                    stations.Insert(0, medRadio);
                }

                return stations;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching stations: {ex.Message}");
                throw;
            }
        }


        public async Task RefreshCacheAsync(string country = null)
        {
            if (country != null)
            {
                await _cacheService.ClearAsync($"stations_{country}");
                await GetStationsByCountryAsync(country);
            }
            else
            {
                await _cacheService.ClearAsync("countries");
                await GetCountriesAsync();
            }
        }
    }
}

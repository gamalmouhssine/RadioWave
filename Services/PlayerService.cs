using Microsoft.JSInterop;
using RadioWave.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioWave.Services
{
    public class PlayerService: IPlayerService
    {
        private readonly IJSRuntime _jsRuntime;
        private RadioStation _currentStation;
        private bool _isPlaying;

        public event Action OnStateChanged;
        public RadioStation CurrentStation => _currentStation;
        public bool IsPlaying => _isPlaying;

        public PlayerService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public async Task PlayStation(RadioStation station)
        {
            try
            {
                if (_currentStation?.Stationuuid == station.Stationuuid && _isPlaying)
                {
                    await StopPlayback();
                    return;
                }

                _currentStation = station;
                await _jsRuntime.InvokeVoidAsync("playAudio", station.Url_resolved);
                _isPlaying = true;
                NotifyStateChanged();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error playing station: {ex.Message}");
                throw;
            }
        }

        public async Task StopPlayback()
        {
            try
            {
                await _jsRuntime.InvokeVoidAsync("stopAudio");
                _isPlaying = false;
                NotifyStateChanged();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error stopping playback: {ex.Message}");
                throw;
            }
        }

        private void NotifyStateChanged() => OnStateChanged?.Invoke();
    }
}

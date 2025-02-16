using RadioWave.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioWave.Services
{
    public interface IPlayerService
    {
        RadioStation CurrentStation { get; }
        bool IsPlaying { get; }
        event Action OnStateChanged;
        Task PlayStation(RadioStation station);
        Task StopPlayback();
    }
}

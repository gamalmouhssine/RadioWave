using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioWave.Models
{
    public class RadioStation
    {
        public string Stationuuid { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Url_resolved { get; set; }
        public string Favicon { get; set; }
        public string Country { get; set; }
        public string Countrycode { get; set; }
        public string Tags { get; set; }
        public int Votes { get; set; }
        public string Codec { get; set; }
        public int Bitrate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioWave.Models
{
    public class Country
    {
        public string Name { get; set; }= string.Empty;
        public string Iso_3166_1 { get; set; }= string.Empty;
        public int Stationcount { get; set; } = 0;
    }
}

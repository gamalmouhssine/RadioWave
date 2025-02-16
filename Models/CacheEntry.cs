using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadioWave.Models
{
    public class CacheEntry<T>
    {
        public T Data { get; set; }
        public DateTime ExpirationTime { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinCache.InMemory.Models
{
    internal class CacheItem
    {
        public object Key { get; }
        public object Value { get; }

        public CacheItem(object x, object y)
        {
            this.Key = x;
            this.Value = y;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinCache.InMemory.Interface
{
    public interface IFinCache
    {
        event CacheItemEvictedEventHandler CacheItemEvicted;
        void AddCache(object key, object value);
        object GetCache(object key);
        void ClearCache();
        void Remove(object key);
        void RemoveFromCache();
        void RaiseEvictedEvent(object key);
    }
}

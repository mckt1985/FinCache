using FinCache.InMemory.Config;
using FinCache.InMemory.Extensions;
using FinCache.InMemory.Interface;
using FinCache.InMemory.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinCache.InMemory
{
    public delegate void CacheItemEvictedEventHandler(object key);

    public class FinCache : IFinCache
    {
        private readonly int Capacity;
        private readonly ConcurrentDictionary<object, object> CacheMap;
        private readonly LinkedList<object> CacheList;
        public event CacheItemEvictedEventHandler CacheItemEvicted;
        private readonly object locker = new object();
        public int ItemCount { get { return CacheMap.Count; } }

        public FinCache(FinCacheConfig config)
        {
            this.Capacity = config.Capacity;
            this.CacheMap = new ConcurrentDictionary<object, object>();
            this.CacheList = new LinkedList<object>();
        }

        public void AddCache(object key, object value)
        {
            if (key.IsNull())
            {
                throw new ArgumentNullException("Key", "Key cannot be null");
            }

            if (value.IsNull())
            {
                throw new ArgumentNullException("Value", "Value cannot be null");
            }
           
            lock (locker)
            {
                if (!CacheMap.ContainsKey(key) && CacheMap.Count == Capacity)
                {
                    RemoveFromCache();
                }
                else if(CacheMap.ContainsKey(key))
                {
                    Remove(key);
                }

                AddAndMapItem(key, value);
            }           
        }

        public object GetCache(object key)
        {
            if (key.IsNull())
            {
                throw new ArgumentNullException("Key", "Key cannot be null.");
            }

            if (CacheMap.ContainsKey(key))
            {
                var value = CacheMap[key];             
                CacheList.Remove(key);

                //add to top of list
                AddAndMapItem(key, value);

                return value;
            }

            return null;
        }

        public void ClearCache()
        {
            if(CacheMap.Count == 0)
            {
                return; // nothing to do
            }

            CacheMap.Clear();
            CacheList.Clear();
        }

        public void Remove(object key)
        {
            if (key.IsNull())
            {
                throw new ArgumentNullException("Key", "Key cannot be null");
            }

            if (CacheMap.ContainsKey(key))
            {
                var item = CacheMap[key];
                CacheList.Remove(item);
                CacheMap.TryRemove(key, out _);
            }
        }

        public void RemoveFromCache()
        {
            lock (locker)
            {
                if(CacheList.Count == 0)
                {
                    return; // nothing to do
                }

                LinkedListNode<object> lastItem = CacheList.Last;

                CacheList.RemoveLast();

                object lastKey = lastItem.Value;

                bool removed = CacheMap.TryRemove(lastKey, out _);

                if (removed)
                {
                    RaiseEvictedEvent(lastKey);
                }
            }            
        }

        public object GetFirstKey()
        {
            if(CacheList.Count == 0)
            {
                return default;
            }

            return CacheList.First.Value;
        }

        private void AddAndMapItem(object key, object value)
        {
            CacheMap[key] = value;
            CacheList.AddFirst(key);
        }
       
        public void RaiseEvictedEvent(object key)
        {
            CacheItemEvicted?.Invoke(key);
        }
    }
}

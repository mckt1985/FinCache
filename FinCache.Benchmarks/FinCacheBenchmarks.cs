using BenchmarkDotNet.Attributes;
using FinCache.InMemory.Interface;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinCache.Benchmarks
{
    [MemoryDiagnoser(false)]
    public class FinCacheBenchmarks
    {
        private readonly IFinCache cache = new FinCache.InMemory.FinCache(new() { Capacity = 100 });
        private readonly IMemoryCache _memoryCache = new MemoryCache(new MemoryCacheOptions());

        [Benchmark]
        public void Add_To_Cache()
        {
            cache.AddCache(1, 1);
        }

        [Benchmark]
        public void Add_To_Cache_Microsoft()
        {
            _memoryCache.Set(1, 1);
        }

        [Benchmark]
        public void Add_Multiple_Entries_To_Cache()
        {
            for (int i = 0; i < 50; i++)
            {
                cache.AddCache(i, i);
            }
        }

        [Benchmark]
        public void Add_Above_Capacity_To_Cache()
        {
            for (int i = 0; i < 1000; i++)
            {
                cache.AddCache(i, i);
            }
        }

        [Benchmark]
        public void Test_Thread_Safety_Of_Cache()
        {
            Parallel.ForEach(Enumerable.Range(1, 1000), i =>
            {
                cache.AddCache(i, i);

            });
        }

        [Benchmark]
        public void Test_Thread_Safety_Of_Microsoft_Cache()
        {
            Parallel.ForEach(Enumerable.Range(1, 1000), i =>
            {
                _memoryCache.Set(i, i);

            });
        }

        [Benchmark]
        public object Add_And_Get_Cache()
        {
            cache.AddCache("test", "test");
            return cache.GetCache("test");
        }

        [Benchmark]
        public void ClearCache()
        {
            cache.ClearCache();
        }
    }
}

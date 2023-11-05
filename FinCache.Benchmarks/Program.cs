using BenchmarkDotNet.Running;
using FinCache.Benchmarks;
using FinCache.InMemory.Interface;



///*************************************************
/// Runs various benchmark tests against Fincache
/// 
BenchmarkRunner.Run<FinCacheBenchmarks>();




//just few initial sequence checks
//TestFinCache();

//void TestFinCache()
//{
//    IFinCache cache = new FinCache.InMemory.FinCache(new() { Capacity = 10 });

//    cache.AddCache(1, 1);
//    cache.AddCache(2, 2);
//    cache.AddCache(3, 3);
//    cache.AddCache(4, 4);
//    cache.AddCache(5, 5);
//    cache.AddCache(6, 6);
//    cache.AddCache(7, 7);
//    cache.AddCache(8, 8);
//    cache.AddCache(9, 9);
//    cache.AddCache(10, 10);
//    //shold remove 1
//    cache.AddCache(11, 11);

//    //should be null
//    var value = cache.GetCache(1);

//    var result = cache.GetCache(11);
    
//    cache.Remove(result);

//}
